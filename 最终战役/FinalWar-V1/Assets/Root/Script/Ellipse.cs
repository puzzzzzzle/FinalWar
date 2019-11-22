using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI;
namespace MoleMole
{
    public enum MYDirection
    {
        //向左
        LEFT,
        //向右
        RIGHT
    }


    public class Ellipse : MonoBehaviour
    {
        //保存需要排序的精灵容器  
        public GameObject[] Sprites;
        public Transform centerPoint;//椭圆的中心点  
        public float anglecheap = 25;//每个方块间的角度偏移  
                                     //保存位置点  
        private List<Vector3> location = new List<Vector3>();
        private float angle = 270;//第一个保证是中心位置的,当前到的角度  
        private float firstangle = 270;//记录第一个角度，用以左右对称  
        private float r = 30;//椭圆的两个弦长  
        private float R = 50;
        //用以判断当前是否所有的按钮回调都执行完了
        List<MYDirection> m_eventList = new List<MYDirection>();
        //显示当前模式
        public Text curMode;
        //保护模式信息
        public string[] modes = new string[3];
        private Vector3 centerPos = new Vector3();
        bool isruningone = false;//是否已经在执行一个移动回调了。
        int i_havemovesprite = 0;//计数用，如果已经移动完所有的方块，那么可以运行下一个按钮回调了
        int size = 0;//即对象数组的一半，奇数为总长度+1/2,偶数为一半<pre name="code" class="csharp">// Use this for initialization  
        Vector2 m_screenpos = new Vector2();//记录手指触屏位置
        void Start()
        {
            //初始化modes
            modes[0] = "AR模式";
            modes[1] = "无尽模式";
            modes[2] = "模拟VR";
            curMode.text = modes[0];
            //初始化size
         // 1 不允许多点触屏
            Input.multiTouchEnabled = false;
            if (Sprites.Length % 2 == 0)
            {
                size = Sprites.Length / 2;
            }
            else
            {
                size = (Sprites.Length + 1) / 2;
            }
            //排序分级显示  
            makespriteSort();
            //重置渲染层级  
            ResetDeep();
        }
        //给这些精灵排序显示  
        void makespriteSort()
        {
            //取出椭圆的中心点  

            centerPos = centerPoint.position;
            //判断该数组的个数奇偶性，如果是偶数，那么需要留出一个来放到对面  
            if (Sprites.Length % 2 == 0)
            {
                //右半边  
                for (int i = 0; i < size; i++)
                {
                    Sprites[i].transform.position = getPosition(angle, centerPos);
                    //       m_rightsprite.Add(Sprites[i]);  
                    angle += anglecheap;
                }
                //第一个已经得是左边了  
                angle = firstangle - anglecheap;
                //左半边  
                for (int i = size; i < Sprites.Length - 1; i++)
                {
                    Sprites[i].transform.position = getPosition(angle, centerPos);
                    angle -= anglecheap;
                    //  m_leftsprite.Add(Sprites[i]);  
                }
                //最后一个  
                Sprites[Sprites.Length - 1].transform.position = getPosition(firstangle - 180, centerPos);
                // m_leftsprite.Add(Sprites[Sprites.Length - 1]);  
                return;
            }
            //如果不是偶数，那么出去中间那个，正好正常显示  
            else
            {
                //右半边  
                for (int i = 0; i < size; i++)
                {
                    Sprites[i].transform.position = getPosition(angle, centerPos);
                    //    m_rightsprite.Add(Sprites[i]);  
                    angle += anglecheap;
                }
                //第一个已经得是左边了  
                angle = firstangle - anglecheap;
                //左半边  
                for (int i = size; i < Sprites.Length; i++)
                {
                    Sprites[i].transform.position = getPosition(angle, centerPos);
                    //  m_leftsprite.Add(Sprites[i]);  
                    angle -= anglecheap;
                }
                return;
            }

        }
        //获取当前角度的坐标  
        Vector3 getPosition(float _angle, Vector3 _centerposition)
        {
            float hudu = (_angle / 180f) * Mathf.PI;
            float cosx = Mathf.Cos(hudu);
            float sinx = Mathf.Sin(hudu);
            float x = _centerposition.x + R * cosx;
            float y = _centerposition.y + r * sinx;
            Vector3 point = new Vector3(x, y, 0);
            //添加到容器保存  
            location.Add(point);
            return point;
        }

        //根据当前左右容器调整所有控件的渲染层级  
        void ResetDeep()
        {
            int dep = 0;
            //右半边
            for (int i = size - 1; i >= 0; i--)
            {
                if (i != 0)
                {
                    Sprites[i].GetComponent<Button>().enabled = false;
                    PlayerPrefs.SetInt(modes[i], 0);
                }
                else
                {
                    Sprites[i].GetComponent<Button>().enabled = true;
                    PlayerPrefs.SetInt(modes[i], 1);

                }
                Sprites[i].GetComponent<Transform>().SetSiblingIndex(dep);
                dep++;
            }
            dep = 0;
            //左半边
            for (int i = Sprites.Length - 1; i >= size; i--)
            {
                Sprites[i].GetComponent<Button>().enabled = false;
                PlayerPrefs.SetInt(modes[i], 0);
                Sprites[i].GetComponent<Transform>().SetSiblingIndex(dep);
                dep++;
            }
        }

        //根据方向来不断执行移动
        void MYButtonEvent(MYDirection _direction)
        {
            for(int i = 0; i < Sprites.Length; i++)
            {
                Sprites[i].GetComponent<Button>().enabled = false;
            }
            //往左移动
            if (_direction == MYDirection.LEFT)
            {
                GameObject temp0 = Sprites[0];//先保存第一个的引用
                string mode = modes[0];//先保留第一个modes值
                if (size <= 0)
                {
                    return;
                }
                //右边全部往前一格
                for (int i = 0; i < size - 1; i++)
                {
                    Sprites[i] = Sprites[i + 1];
                    modes[i] = modes[i + 1];
                }
                //右边最后一个直接赋值
                Sprites[size - 1] = Sprites[Sprites.Length - 1];
                modes[size - 1] = modes[modes.Length - 1];
                //左边全部往后一格
                for (int i = Sprites.Length - 1; i > size; i--)
                {
                    Sprites[i] = Sprites[i - 1];
                    modes[i] = modes[i - 1];
                }
                //左边第一个直接赋值
                Sprites[size] = temp0;
                modes[size] = mode;
                //给Text赋值
                curMode.text = modes[0];
                //移动方块
                Move(MYDirection.LEFT);
                return;
            }
            //往右移动
            else
            {
                GameObject temp0 = Sprites[size];//先保存左边第一个的引用
                string mode = modes[size];//先保存左边第一个模式
                if (size <= 0)
                {
                    return;
                }
                //左边全部往前一格
                for (int i = size; i < Sprites.Length - 1; i++)
                {
                    Sprites[i] = Sprites[i + 1];
                    modes[i] = modes[i + 1];
                }
                //左边最后一个直接赋值
                Sprites[Sprites.Length - 1] = Sprites[size - 1];
                modes[modes.Length - 1] = modes[size - 1];
                //右边全部往后一格
                for (int i = size - 1; i > 0; i--)
                {
                    Sprites[i] = Sprites[i - 1];
                    modes[i] = modes[i - 1];
                }
                //左边第一个直接赋值
                Sprites[0] = temp0;
                modes[0] = mode;
                //给Text赋值
                curMode.text = modes[0];
                //左边全部往后一格
                //移动方块
                Move(MYDirection.RIGHT);
                return;
            }
        }

        void Update()
        {
#if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
        MobileInput();
#else
            DesktopInput();
#endif
        }
        //andriod 触屏操作
        void MobileInput()
        {
            if (Input.touchCount <= 0)
            {
                return;
            }
            //开始触屏
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                //记录手指触屏的位置
                m_screenpos = Input.touches[0].position;
            }
            //手指离开屏幕
            if (Input.touches[0].phase == TouchPhase.Ended && Input.touches[0].phase != TouchPhase.Canceled)
            {
                Vector2 pos = Input.touches[0].position;
                if (m_screenpos.x > pos.x)
                {
                    //手指向左滑动
                    //添加到容器里，用以多次点击时延迟执行
                    m_eventList.Add(MYDirection.LEFT);
                    doEvent();
                }
                else if (m_screenpos.x < pos.x)
                {
                    //手指向右滑动
                    //添加到容器里，用以多次点击时延迟执行
                    m_eventList.Add(MYDirection.RIGHT);
                    doEvent();
                }
            }
        }

        void DesktopInput()
        {

            //鼠标左键
            if (Input.GetMouseButton(0))
            {
                //记录鼠标左键移动距离
                float mx = Input.GetAxis("Mouse X");
                //鼠标左移
                if (mx < -1.0)
                {
                    //添加到容器里，用以多次点击时延迟执行
                    m_eventList.Add(MYDirection.LEFT);
                    doEvent();
                }
                else if (mx > 1.0)
                {
                    //添加到容器里，用以多次点击时延迟执行
                    m_eventList.Add(MYDirection.LEFT);
                    doEvent();
                }
            }

        }

        //集中判断执行哪个按钮的回调函数
        public void doEvent()
        {
            if (isruningone == true)//如果已经在执行着一个了，那么就不再执行
            {
                return;
            }
            //如果所有的都执行完了，那么跳出
            if (m_eventList.Count <= 0)
            {
                return;
            }
            isruningone = true;//设置为true，锁定，防止多次执行
                               //如果没有，那么取出一个来执行
            MYButtonEvent(m_eventList[0]);
            m_eventList.RemoveAt(0);
        }
        //移动的函数
        void Move(MYDirection _direction)
        {

            int size = Sprites.Length;
            for (int i = 0; i < size; i++)
            {
                //使用itween接口实现移动效果
                Hashtable table = gethashtable(location[i]);
                // iTween.MoveTo(Sprites[i],table);
                List<Vector3> newlist = getListend(Sprites[i].transform.position, location[i], _direction, i);
                table.Add("Ellipse", newlist);
                ManagerEffective.MoveTo(Sprites[i], table, newlist);
            }
            StartCoroutine(judgeifneedrestdepth());
        }
        Hashtable gethashtable(Vector3 position)
        {
            Hashtable args = new Hashtable();
            //这里是设置类型，iTween的类型又很多种，在源码中的枚举EaseType中
            //例如移动的特效，先震动在移动、先后退在移动、先加速在变速、等等
            args.Add("easeType", iTween.EaseType.easeInOutExpo);

            //移动的速度，
            // args.Add("speed", 10f);
            //移动的整体时间。如果与speed共存那么优先speed
            args.Add("time", 0.21f);
            //这个是处理颜色的。可以看源码的那个枚举。
            // args.Add("NamedValueColor", "_SpecColor");
            //延迟执行时间
            args.Add("delay", 0f);
            //移动的过程中面朝一个点
            //args.Add("looktarget", Vector3.zero);

            //三个循环类型 none loop pingPong (一般 循环 来回)	
            //args.Add("loopType", "none");
            //args.Add("loopType", "loop");	
            args.Add("loopType", "one");

            //处理移动过程中的事件。
            //开始发生移动时调用AnimationStart方法，5.0表示它的参数
            //   args.Add("onstart", "AnimationStart");
            //  args.Add("onstartparams", 5.0f);
            //设置接受方法的对象，默认是自身接受，这里也可以改成别的对象接受，
            //那么就得在接收对象的脚本中实现AnimationStart方法。
            //    args.Add("onstarttarget", gameObject);


            //移动结束时调用，参数和上面类似
            args.Add("oncomplete", "judgeifneedgotoevent");
            //args.Add("oncompleteparams", "end");
            args.Add("oncompletetarget", gameObject);


            //移动中调用，参数和上面类似
            // args.Add("onupdate", "judgeifneedrestdepth");
            // args.Add("onupdatetarget", gameObject);
            //   args.Add("onupdateparams", true);
            // x y z 标示移动的位置。
            args.Add("position", position);
            return args;
        }
        //计算是否需要重置渲染层级
        public IEnumerator judgeifneedrestdepth()
        {
            yield return new WaitForSeconds(0.1f);
            ResetDeep();
        }
        //判断是否该执行回调函数了
        public void judgeifneedgotoevent()
        {
            i_havemovesprite++;
            if (i_havemovesprite >= Sprites.Length)
            {
                isruningone = false;
                i_havemovesprite = 0;
                doEvent();
            }
        }
        //根据点坐标获取椭圆的角度
        int getAngle(Vector3 point)
        {

            int angle = 0;
            int x = (int)(point.x - centerPos.x);
            int y = (int)(point.y - centerPos.y);
            float _angle = 0;
            if (x >= 0 && y >= 0)
            {
                _angle = Mathf.Acos(x / R);
                _angle = (_angle / Mathf.PI) * 180;
            }
            else if (x < 0 && y > 0)
            {
                _angle = Mathf.Acos(x / R);
                _angle = (_angle / Mathf.PI) * 180;
            }
            else if (x < 0 && y <= 0)
            {
                _angle = Mathf.Acos(x / R);
                _angle = (_angle / Mathf.PI) * 180;
                _angle = 360 - _angle;
            }
            else if (x >= 0 && y < 0)
            {
                _angle = Mathf.Acos(x / R);
                _angle = (_angle / Mathf.PI) * 180;
                _angle = 360 - _angle;
            }
            angle = (int)_angle;
            return angle;
        }
        Vector3 GetPoint(float _angle, Vector3 _centerposition)
        {
            float hudu = (_angle / 180f) * Mathf.PI;
            float cosx = Mathf.Cos(hudu);
            float sinx = Mathf.Sin(hudu);
            float x = _centerposition.x + R * cosx;
            float y = _centerposition.y + r * sinx;
            Vector3 point = new Vector3(x, y, 0);
            return point;
        }
        //Test
        List<Vector3> getListend(Vector3 Start_point, Vector3 End_point, MYDirection _direction, int index)
        {
            List<Vector3> newlist = new List<Vector3>();
            int endAngle = getAngle(End_point);
            int StartAngle = getAngle(Start_point);
            int loacleangle = 0;
            if (_direction == MYDirection.LEFT)
            {
                if (index == (size - 1) && endAngle < 360 && endAngle > 270)//如果是左边最后一个
                {
                    loacleangle = -(360 - endAngle + StartAngle) / 3;
                }
                else
                {
                    if (Sprites.Length == 2 && endAngle <= 360 && endAngle >= 270)
                    {
                        loacleangle = (StartAngle - endAngle) / 3;
                    }
                    else
                    {
                        loacleangle = (endAngle - StartAngle) / 3;
                    }

                }

            }
            else
            {
                if (index == (Sprites.Length - 1) && StartAngle < 360 && StartAngle > 270)//如果是右边最后一个
                {
                    loacleangle = (360 - StartAngle + endAngle) / 3;
                }
                else
                {
                    if (Sprites.Length == 2 && StartAngle <= 360 && StartAngle >= 270)
                    {
                        loacleangle = (StartAngle - endAngle) / 3;
                    }
                    else
                    {
                        loacleangle = (endAngle - StartAngle) / 3;
                    }

                }
            }
            float angle = (float)(StartAngle + loacleangle);
            Vector3 centPos = centerPos;
            Vector3 tem = GetPoint(angle, centPos);
            newlist.Add(tem);
            angle += loacleangle;
            tem = GetPoint(angle, centPos);
            newlist.Add(tem);
            newlist.Add(End_point);
            return newlist;
        }
    }
}