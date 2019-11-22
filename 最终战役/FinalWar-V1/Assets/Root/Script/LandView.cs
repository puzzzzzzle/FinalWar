using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using artest.AIThirdPerson;
namespace MoleMole
{
    public class LandContext : BaseContext
    {
        public LandContext() : base(UIType.Land)
        {

        }
    }

    public class LandView : AnimateView
    {
        //持有用户名和密码这两个输入框的对象

        [SerializeField]
        public InputField Username;
        [SerializeField]
        public InputField Password;
        [SerializeField]
        private Button _buttonLand;
        [SerializeField]
        private Button _buttonReigister;


        //定义访问JSP登录表单的get方式访问路径   登录检测
        //private string Url = "http://192.168.234.1:8080/Land.do?";
        private string Url = "http://47.93.195.255:8080/Land.do?";
        //定义访问JSP登录表单的get方式访问路径    获取数据
        //private string getUrl = "http://192.168.234.1:8080/GetData.do?";
        private string getUrl = "http://47.93.195.255:8080/GetData.do?";
        //定义访问JSP登录表单的get方式访问路径    上传最新数据
        //private string subUrl = "http://192.168.234.1:8080/SaveData.do?";
        private string subUrl = "http://47.93.195.255:8080/SaveData.do?";

        XmlPlayerInformation xml;


        //存储本地信息
        PlayerInfromation local;
        //判断本地储存是否已经存在当前用户
        public PlayerInfromation haveLocal(string id)
        {
            return xml.selectUser(id);
        }

        //当按钮被点击
        public void LoginButtonOnClick()
        {
            PlayerPrefs.SetString("temp", "land");

            if (Username.text.Length == 0 && Password.text.Length == 0)
            {
                PlayerPrefs.SetInt("SelectView", 1);
                Singleton<LandContextManager>.Instance.Push(new PopContext(),"账号密码不能为空");
                return;
            }
            if (Username.text.Length == 0)
            {
                PlayerPrefs.SetInt("SelectView", 1);
                Singleton<LandContextManager>.Instance.Push(new PopContext(), "账号不能为空");
                return;
            }
            if (Password.text.Length == 0)
            {
                PlayerPrefs.SetInt("SelectView", 1);
                Singleton<LandContextManager>.Instance.Push(new PopContext(), "密码不能为空");
                return; 
            }
            //判断passwrod是否都由字母或数字组成
            string pattern = @"^[a-zA-Z0-9]*$";
            if (!System.Text.RegularExpressions.Regex.IsMatch(Password.text, pattern)){
                PlayerPrefs.SetInt("SelectView", 1);
                Singleton<LandContextManager>.Instance.Push(new PopContext(), "密码只能有字母和数字组成");
                return;
            }
            //向服务器传递的参数
            string parameter = "";
            parameter += "UserName=" + Username.text + "&";
            parameter += "PassWord=" + Password.text;

            //开始传递
            StartCoroutine(login(Url + parameter));

        }
        //访问JSP服务器获得玩家数据
        IEnumerator getData(string path)
        {
            WWW www = new WWW(path);
            yield return www;
            //如果发生错误，打印这个错误
            if (www.error != null)
            {
                Debug.Log(www.error);
            }
            else
            {
                //如果服务器返回的是true

                //提取数据
                    char[] chars = www.text.ToCharArray();
                    List<string> dbData = new List<string>();
                    StringBuilder data = new StringBuilder();
                    for (int i = 0; i < chars.Length; i++)
                    {
                        if (chars[i] != ';')
                        {
                            data.Append(chars[i]);
                        }
                        else
                        {
                            dbData.Add(data.ToString());
                            data.Length = 0;
                        }
                    }
                        local = new PlayerInfromation();
                        local.id = Username.text + "id";
                        local.name = dbData[0];
                        local.score = System.Convert.ToDouble(dbData[1]);
                        local.money = System.Convert.ToDouble(dbData[2]);
                        local.level = System.Convert.ToInt32(dbData[3]);
                        local.castle = System.Convert.ToInt32(dbData[4]);
                        local.defence = System.Convert.ToInt32(dbData[5]);
                        local.propone = System.Convert.ToInt32(dbData[6]);
                        local.proptwo = System.Convert.ToInt32(dbData[7]);
                        local.propthree = System.Convert.ToInt32(dbData[8]);
                        xml.addUser(local);
                        Application.LoadLevel("Test");

            }
        }

        //访问JSP服务器 上传数据
        IEnumerator subData(string path)
        {
            WWW www = new WWW(path);
            yield return www;
            //如果发生错误，打印这个错误
            if (www.error != null)
            {
                Debug.Log(www.error);
            }
            else
            {

                Application.LoadLevel("Test");
      
            }
        }

        //访问JSP服务器 监查登录
        IEnumerator login(string path)
        {
            WWW www = new WWW(path);
            yield return www;
            //如果发生错误，打印这个错误
            if (www.error != null)
            {
                Debug.Log(www.error);
            }
            else
            {
                //如果服务器返回的是true
                if (www.text.Equals("true"))
                {
                    //登陆成功
                    PlayerPrefs.SetString("player", Username.text);
                    PlayerPrefs.SetString("id", Username.text + "id");
                    //获取数据
                    //向服务器传递的参数

                    //如果没有本地记录，从数据库里调回数据创建
                    string id = Username.text + "id";
                    local = haveLocal(id);
                    if (local == null)
                    {


                        //创建新的本地记录
                        //获取服务器数据
                        string parameter = "";
                        parameter += "UserName=" + Username.text;
                        StartCoroutine(getData(getUrl + parameter));
                    }else
                    {
                        string parameter = "";
                        string score = local.score.ToString();
                        string money = local.money.ToString();
                        string level= local.level.ToString();
                        string castle = local.castle.ToString();
                        string defence = local.defence.ToString();
                        string propone = local.propone.ToString();
                        string proptwo = local.proptwo.ToString();
                        string propthree = local.propthree.ToString();
                        parameter += "UserName=" + Username.text + "&";
                        parameter += "Score=" + score + "&";
                        parameter += "Money=" + money + "&";
                        parameter += "Level=" + level + "&";
                        parameter += "Castle=" + castle + "&";
                        parameter += "Defence=" + defence + "&";
                        parameter += "Propone=" + propone + "&";
                        parameter += "Proptwo=" + proptwo + "&";
                        parameter += "Propthree=" + propthree;
                        StartCoroutine(subData(subUrl + parameter));
                    }


                }
                else
                {
                    //否则登录失败
                    PlayerPrefs.SetInt("SelectView", 1);
                    Singleton<LandContextManager>.Instance.Push(new PopContext(), "账号或者密码不正确");
                }
            }
        }


        //当按钮被点击
        public void ReigisterButtonOnClick() {          
            Singleton<LandContextManager>.Instance.Push(new ReigisterContext());
        }

        //直接开始游戏按钮
        public void StartButtonOnClick()
        {
            PlayerPrefs.SetString("temp", "default");
            local = haveLocal("default");

            if (local == null)
            {
                local = new PlayerInfromation();
                local.name = "default";
                local.id = "default";
                local.money = 0.0;
                local.score = 0.0;
                local.level = 0;
                local.castle = 0;
                local.defence =0;
                local.propone =0;
                local.proptwo =0;
                local.propthree = 0;
                xml.addUser(local);
            }
            Application.LoadLevel("Test");
        }

        void Start()
        {
            xml = new XmlPlayerInformation();

        }

        public override void OnEnter(BaseContext context)
        {

            base.OnEnter(context);
        }

        public override void OnExit(BaseContext context)
        {
            base.OnExit(context);
        }

        public override void OnPause(BaseContext context)
        {
            base.OnPause(context);
        }

        public override void OnResume(BaseContext context)
        {
            base.OnResume(context);
        }





    }
}