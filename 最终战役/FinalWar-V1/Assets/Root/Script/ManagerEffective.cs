using UnityEngine;
using System.Collections;
using System;
using System.Reflection;

using System.Collections.Generic;
namespace MoleMole
{
    public class ManagerEffective : MonoBehaviour
    {
        public enum LoopType//循环方式
        {
            one,//一次
            loop,//循环
            pingpong//来回
        }
        public static ArrayList ef_tive = new ArrayList();
        //椭圆测试
        bool m_isEllips = false;
        List<Vector3> TargetPos = new List<Vector3>();
        float nowtimelimit;
        int count = 0;

        //多少秒内完成，开始前等待时间
        public float time, delay;
        //循环方式
        public LoopType loopType;

        public bool isRunning;//判断是否在更新位置




        //代理函数
        private delegate void ApplyMthood();
        private ApplyMthood apply;//代理函数对象
        private Vector3[] vector3s;//用以保存当前的进度，目标的位置信息等
                                   //当前时间，进度
        private float runningTime, percentage;
        private float lastRealTime; // Added by PressPlay
        private bool useRealTime; // Added by PressPlay
        private Hashtable myhashtable;//当前的hashtable
        private string Actiontype;//动作类型

        public static class Defaults//默认值
        {
            public static float time = 0.5f;//默认移动时间
            public static float delaytime = 0;
            public static LoopType loopType = LoopType.one;
            public static string Actiontype = "MoveTo";
        }
        //直接外调的函数，直接移动
        public static void MoveTo(GameObject _object, Vector3 position, float time)
        {
            MoveTo(_object, Hash("position", position, "time", time));
        }
        //通过哈希表来移动的函数,直接移动
        public static void MoveTo(GameObject _object, Hashtable args)
        {
            args = CleanArgs(args);
            if (args.Contains("position"))
            {
                if (args["position"].GetType() == typeof(Transform))
                {
                    Transform transform = (Transform)args["position"];
                    args["position"] = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                    args["rotation"] = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
                    args["scale"] = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
                }
            }
            args.Add("ActionType", "MoveTo");
            Lunch(_object, args);
        }
        //椭圆移动效果
        public static void MoveTo(GameObject _object, Vector3 position, float time, List<Vector3> _date)
        {

            MoveTo(_object, Hash("position", position, "time", time), _date);
        }
        //通过哈希表来移动的函数，椭圆移动效果
        public static void MoveTo(GameObject _object, Hashtable args, List<Vector3> _date)
        {
            args = CleanArgs(args);
            if (args.Contains("position"))
            {
                if (args["position"].GetType() == typeof(Transform))
                {
                    Transform transform = (Transform)args["position"];
                    args["position"] = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                    args["rotation"] = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
                    args["scale"] = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
                }
            }
            args.Add("ActionType", "MoveTo");
            Lunch(_object, args);
        }
        public static void Lunch(GameObject _object, Hashtable args)
        {
            if (!args.Contains("target"))
            {
                args["target"] = _object;
            }
            ef_tive.Insert(0, args);
            _object.AddComponent<ManagerEffective>();
        }
        //根据数组转换成一个哈希表
        public static Hashtable Hash(params object[] args)
        {
            Hashtable hashTable = new Hashtable(args.Length / 2);
            if (args.Length % 2 != 0)
            {
                return null;
            }
            else
            {
                int i = 0;
                while (i < args.Length - 1)
                {
                    hashTable.Add(args[i], args[i + 1]);
                    i += 2;
                }
                return hashTable;
            }
        }
        //保证都是float类型的
        //cast any accidentally supplied doubles and ints as floats as iTween only uses floats internally and unify parameter case:
        static Hashtable CleanArgs(Hashtable args)
        {
            Hashtable argsCopy = new Hashtable(args.Count);
            Hashtable argsCaseUnified = new Hashtable(args.Count);

            foreach (DictionaryEntry item in args)
            {
                argsCopy.Add(item.Key, item.Value);
            }

            foreach (DictionaryEntry item in argsCopy)
            {
                if (item.Value.GetType() == typeof(System.Int32))
                {
                    int original = (int)item.Value;
                    float casted = (float)original;
                    args[item.Key] = casted;
                }
                if (item.Value.GetType() == typeof(System.Double))
                {
                    double original = (double)item.Value;
                    float casted = (float)original;
                    args[item.Key] = casted;
                }
            }

            //unify parameter case:
            foreach (DictionaryEntry item in args)
            {
                argsCaseUnified.Add(item.Key.ToString().ToLower(), item.Value);
            }

            //swap back case unification:
            args = argsCaseUnified;
            return args;
        }
        void Awake()
        {

            RetrieveArgs();//初始化哈希表中移动参数
            lastRealTime = Time.realtimeSinceStartup; // Added by PressPlay
        }

        // Use this for initialization
        void Start()
        {
            ConflictCheck();
            if (m_isEllips == true)
            {
                StartCoroutine(EllipseDelay());
                return;
            }
            StartCoroutine(EFDelay());
        }
        // Update is called once per frame
        void Update()
        {
            if (isRunning)
            {
                if (m_isEllips)
                {
                    if (percentage < 1f)
                    {
                        EllipseUpdate();
                    }
                    else
                    {
                        EllipseComplete();
                    }
                }
                else
                {
                    if (percentage < 1f)
                    {
                        actionUpdate();
                    }
                    else
                    {
                        actionComplete();
                    }
                }

            }
        }
        //椭圆部分
        IEnumerator EllipseDelay()
        {    //间隔delaytime之后开始执行
            yield return new WaitForSeconds(delay);
            //根据类型设置代理函数
            switch (Actiontype)
            {
                case "MoveTo":
                    EllipseGenerateMoveToTargets();
                    apply = new ApplyMthood(EllipseApplyMoveToTargets);
                    break;
                case "ScaleTo":
                    break;
            }
            //初始化必要数据
            EllipseActionStart();
        }
        void EllipseActionStart()
        {
            isRunning = true;//开始运动了

        }
        void EllipseGenerateMoveToTargets()
        {
            Vector3 endlPos = new Vector3();
            if (myhashtable.Contains("position"))
            {
                if (myhashtable["position"].GetType() == typeof(Transform))
                {
                    Transform trans = (Transform)myhashtable["position"];
                    endlPos = trans.position;
                }
                else if (myhashtable["position"].GetType() == typeof(Vector3))
                {
                    endlPos = (Vector3)myhashtable["position"];
                }
                else
                {//第三种方式，直接输入xyz表达目的值     
                    if (myhashtable.Contains("x"))
                    {
                        endlPos.x = (float)myhashtable["x"];
                    }
                    if (myhashtable.Contains("y"))
                    {
                        endlPos.y = (float)myhashtable["y"];
                    }
                    if (myhashtable.Contains("z"))
                    {
                        endlPos.z = (float)myhashtable["z"];
                    }
                }
            }
            /*int endAngle = getAngle(endlPos);
            int StartAngle = getAngle(transform.position);
           int  loacleangle=0;
            if(ef_date._direction==MYDirection.LEFT){
                if(endAngle>270 && StartAngle<90)
                {
                loacleangle=-(360-endAngle+StartAngle)/3;
                 }
                else{
                    loacleangle=(endAngle-StartAngle)/3;
                }
            }
            else{
                  loacleangle=(endAngle-StartAngle)/3;
            }
            float  angle=(float)(StartAngle+loacleangle);
            Vector3 centPos=ef_date.centPos;
            Vector3 tem = getPosition(angle, centPos);
            TargetPos.Add(tem);
            angle+=loacleangle;
            tem =getPosition(angle, centPos);
            TargetPos.Add(tem);
             TargetPos.Add(endlPos);*/

            //values holder [0] from, [1] to, [2] calculated value from ease equation:
            vector3s = new Vector3[3];//即第一个是出发点，第二个是目标点，第三个时当前点
            vector3s[0] = transform.position; ;
            vector3s[1] = TargetPos[0];
            float itim = (1.0f / (float)TargetPos.Count);
            nowtimelimit = itim * time;
            TargetPos.RemoveAt(0);
        }
        void EllipseApplyMoveToTargets()
        {
            //calculate:
            vector3s[2].x = linear(vector3s[0].x, vector3s[1].x, percentage);
            vector3s[2].y = linear(vector3s[0].y, vector3s[1].y, percentage);
            vector3s[2].z = linear(vector3s[0].z, vector3s[1].z, percentage);

            transform.position = vector3s[2];
            //dial in:
            if (percentage == 1)
            {
                transform.position = vector3s[1];
            }
        }
        //移动的update
        void EllipseUpdate()
        {
            apply();
            runningTime += Time.deltaTime;
            percentage = runningTime / nowtimelimit;
        }
        //重新来一个移动
        void ResetEllipseMove()
        {
            vector3s[0] = transform.position; ;
            vector3s[1] = TargetPos[0];
            // nowtimelimit = (1 / TargetPos.Count) * time;
            TargetPos.RemoveAt(0);
        }


        //移动到当前位置后，判断是否完成所有移动
        void EllipseComplete()
        {
            isRunning = false;
            percentage = 1;
            apply();

            if (TargetPos.Count == 0)
            {
                Dispose();
                CallBack("oncomplete");
            }
            else
            {
                percentage = 0;
                runningTime = 0;
                ResetEllipseMove();
                EllipseActionStart();
            }

        }



        //
        #region #1 Normal
        void ConflictCheck()
        {//if a new iTween is about to run and is of the same type as an in progress iTween this will destroy the previous if the new one is NOT identical in every way or it will destroy the new iTween if they are:	
            Component[] tweens = GetComponents(typeof(ManagerEffective));
            foreach (ManagerEffective item in tweens)
            {
                if (item.Actiontype == "value")
                {
                    return;
                }
                else if (item.isRunning && item.Actiontype == Actiontype)
                {

                    //step 1: check for length first since it's the fastest:
                    if (item.myhashtable.Count != myhashtable.Count)
                    {
                        item.Dispose();
                        return;
                    }
                    //step 2: side-by-side check to figure out if this is an identical tween scenario to handle Update usages of iTween:
                    foreach (DictionaryEntry currentProp in myhashtable)
                    {
                        if (!item.myhashtable.Contains(currentProp.Key))
                        {
                            item.Dispose();
                            return;
                        }
                        else
                        {
                            if (!item.myhashtable[currentProp.Key].Equals(myhashtable[currentProp.Key]))
                            {//if we aren't comparing ids and something isn't exactly the same replace the running iTween: 
                                item.Dispose();
                                return;
                            }
                        }
                    }

                    //step 3: prevent a new iTween addition if it is identical to the currently running iTween
                    Dispose();
                    //Destroy(this);	
                }
            }
        }

        IEnumerator EFDelay()
        {//间隔delaytime之后开始执行
            yield return new WaitForSeconds(delay);
            //初始化必要数据
            ActionStart();
        }
        void ActionStart()
        {
            isRunning = true;//开始运动了
            StartInit();
        }
        void StartInit()
        {//根据类型设置代理函数
            switch (Actiontype)
            {
                case "MoveTo":
                    GenerateMoveToTargets();
                    apply = new ApplyMthood(ApplyMoveToTargets);
                    break;
                case "ScaleTo":
                    break;
            }
        }
        void GenerateMoveToTargets()
        {
            //values holder [0] from, [1] to, [2] calculated value from ease equation:
            vector3s = new Vector3[3];//即第一个是出发点，第二个是目标点，第三个时当前点
            vector3s[0] = vector3s[1] = transform.position;
            if (myhashtable.Contains("position"))
            {
                if (myhashtable["position"].GetType() == typeof(Transform))
                {
                    Transform trans = (Transform)myhashtable["position"];
                    vector3s[1] = trans.position;
                }
                else if (myhashtable["position"].GetType() == typeof(Vector3))
                {
                    vector3s[1] = (Vector3)myhashtable["position"];
                }
                else
                {//第三种方式，直接输入xyz表达目的值     
                    if (myhashtable.Contains("x"))
                    {
                        vector3s[1].x = (float)myhashtable["x"];
                    }
                    if (myhashtable.Contains("y"))
                    {
                        vector3s[1].x = (float)myhashtable["y"];
                    }
                    if (myhashtable.Contains("z"))
                    {
                        vector3s[1].x = (float)myhashtable["z"];
                    }
                }
            }
        }
        void ApplyMoveToTargets()
        {
            //calculate:
            vector3s[2].x = linear(vector3s[0].x, vector3s[1].x, percentage);
            vector3s[2].y = linear(vector3s[0].y, vector3s[1].y, percentage);
            vector3s[2].z = linear(vector3s[0].z, vector3s[1].z, percentage);

            transform.position = vector3s[2];
            //dial in:
            if (percentage == 1)
            {
                transform.position = vector3s[1];
            }
        }
        //获取当前的目标位置
        private float linear(float start, float end, float value)
        {
            return Mathf.Lerp(start, end, value);
        }


        //动作update
        void actionUpdate()
        {
            apply();
            UpdatePercentage();
        }
        void UpdatePercentage()
        {

            runningTime += Time.deltaTime;
            percentage = runningTime / time;
        }
        //动作完成
        void actionComplete()
        {
            isRunning = false;
            percentage = 1;
            apply();

            Dispose();
            CallBack("oncomplete");
        }
        //消除脚本
        void Dispose()
        {
            for (int i = 0; i < ef_tive.Count; i++)
            {
                Hashtable tweenEntry = (Hashtable)ef_tive[i];
                if (tweenEntry == myhashtable)
                {
                    ef_tive.Remove(tweenEntry);
                    break;
                }
            }
            Destroy(this);
        }
        //初始化哈希表所有需要的信息
        void RetrieveArgs()
        {

            foreach (Hashtable item in ef_tive)
            {
                if ((GameObject)item["target"] == gameObject)
                {
                    myhashtable = item;
                    break;
                }
            }

            if (myhashtable.Contains("time"))
            {
                time = (float)myhashtable["time"];
            }
            else
            {
                time = Defaults.time;
            }
            if (myhashtable.Contains("delay"))
            {
                delay = (float)myhashtable["delay"];
            }
            else
            {
                delay = Defaults.delaytime;
            }

            if (myhashtable.Contains("looptype"))
            {
                //allows loopType to be set as either an enum(C# friendly) or a string(JS friendly), string case usage doesn't matter to further increase usability:
                if (myhashtable["looptype"].GetType() == typeof(LoopType))
                {
                    loopType = (LoopType)myhashtable["looptype"];
                }
                else
                {
                    try
                    {
                        loopType = (LoopType)Enum.Parse(typeof(LoopType), (string)myhashtable["looptype"], true);
                    }
                    catch
                    {
                        loopType = ManagerEffective.LoopType.one;
                    }
                }
            }
            else
            {
                loopType = ManagerEffective.LoopType.one;
            }
            if (myhashtable.Contains("ActionType"))
            {
                Actiontype = (string)myhashtable["ActionType"];
            }
            else
            {
                Actiontype = Defaults.Actiontype;
            }
            if (myhashtable.Contains("ellipse"))
            {
                m_isEllips = true;
                TargetPos = (List<Vector3>)myhashtable["ellipse"];
            }

        }
        //当运动完之后的回调函数
        void CallBack(string callbackType)
        {
            if (myhashtable.Contains(callbackType) && !myhashtable.Contains("ischild"))
            {
                //establish target:
                GameObject target;
                if (myhashtable.Contains(callbackType + "target"))
                {
                    target = (GameObject)myhashtable[callbackType + "target"];
                }
                else
                {
                    target = gameObject;
                }

                //throw an error if a string wasn't passed for callback:
                if (myhashtable[callbackType].GetType() == typeof(System.String))
                {
                    target.SendMessage((string)myhashtable[callbackType], (object)myhashtable[callbackType + "params"], SendMessageOptions.DontRequireReceiver);
                }
                else
                {
                    // Destroy(this);
                }
            }
        }
        #endregion
    }
}