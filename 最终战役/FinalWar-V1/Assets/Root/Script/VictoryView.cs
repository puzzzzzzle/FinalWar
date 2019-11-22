using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using artest.AIThirdPerson;
using System.Text;
using UnityEngine.SceneManagement;

namespace MoleMole
{
    public class VictoryContext : BaseContext
    {
        public VictoryContext()
            : base(UIType.Victory)
        {

        }
    }

    public class VictoryView : AnimateView
    {
        //分数
        [SerializeField]
        public Text Score;
        //金钱
        [SerializeField]
        public Text Money;
        //定义访问JSP登录表单的get方式访问路径
        //private string Url = "http://192.168.234.1:8080/SaveData.do?";
        private string Url = "http://47.93.195.255:8080/SaveData.do?";
        //保存玩家名字
        string Player;

        //定义访问JSP登录表单的get方式访问路径    获取数据
        //private string getUrl = "http://192.168.234.1:8080/GetData.do?";
        private string getUrl = "http://47.93.195.255:8080/GetData.do?";

        //定义访问JSP登录表单的get方式访问路径    上传最新数据
        //private string subUrl = "http://192.168.234.1:8080/SaveData.do?";
        private string subUrl = "http://47.93.195.255:8080/SaveData.do?";

        //判断本地储存是否已经存在当前用户
        XmlPlayerInformation xml;
        PlayerInfromation local;
        public override void OnEnter(BaseContext context)
        {

            base.OnEnter(context);
        }

        void Start()
        {
            string score = PlayerPrefs.GetString("score");
            string money = PlayerPrefs.GetString("money");
            Score.text = score;
            Money.text = money;
            xml = new XmlPlayerInformation();
            string mode = PlayerPrefs.GetString("temp");
            if (mode.Equals("default"))
            {
                local = xml.selectUser("default");
                if (local == null)
                {
                    local = new PlayerInfromation();
                    local.name = "default";
                    local.id = "default";
                    local.money = 0.0;
                    local.score = 0.0;
                    local.level = 0;
                    local.castle = 0;
                    local.defence = 0;
                    local.propone = 0;
                    local.proptwo = 0;
                    local.propthree = 0;
                    xml.addUser(local);
                }
                
                //当前分数
                double curscore = System.Convert.ToDouble(PlayerPrefs.GetString("score"));
                //当前钱数
                double curmoney = System.Convert.ToDouble(PlayerPrefs.GetString("money"));
                local.score += curscore;
                local.money += curmoney;
                xml.changePlayer(local);
            }
            else
            {
                string player = PlayerPrefs.GetString("player");
                string id = player + "id";
                local = xml.selectUser(id);
                string parameter = "";
                if (local == null)
                {
                    parameter += "UserName=" + player;
                    //创建新的本地记录
                    StartCoroutine(getData(getUrl + parameter));
                }
                //当前分数
                double curscore = System.Convert.ToDouble(PlayerPrefs.GetString("score"));
                //当前钱数
                double curmoney = System.Convert.ToDouble(PlayerPrefs.GetString("money"));
                local.score += curscore;
                local.money += curmoney;
                xml.changePlayer(local);
                string s = local.score.ToString();
                string m = local.money.ToString();
                string level = local.level.ToString();
                string castle = local.castle.ToString();
                string defence = local.defence.ToString();
                string propone = local.propone.ToString();
                string proptwo = local.proptwo.ToString();
                string propthree = local.propthree.ToString();
                parameter += "UserName=" + player + "&";
                parameter += "Score=" + s + "&";
                parameter += "Money=" + m + "&";
                parameter += "Level=" + level + "&";
                parameter += "Castle=" + castle + "&";
                parameter += "Defence=" + defence + "&";
                parameter += "Propone=" + propone + "&";
                parameter += "Proptwo=" + proptwo + "&";
                parameter += "Propthree=" + propthree;
                StartCoroutine(subData(subUrl + parameter));
            }
      
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
                local.id = dbData[0] + "id";
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

            }
        }



        public override void OnExit(BaseContext context)
        {
            PlayerPrefs.SetInt("Shop", 1);
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
        //重新开始游戏
        public void RestartCallBack()
        {
            if ((PlayerPrefs.GetInt("SelectLevel") <= 4))
                SceneManager.LoadScene("scene/gameScene");
            else
                SceneManager.LoadScene("scene/TuoLuoYi");
            //TODO
        }
        //进入下一关
        public void NextCallBACK()
        {
            if (PlayerPrefs.GetInt("SelectLevel") <= 3)
            {
                PlayerPrefs.SetInt("SelectLevel", PlayerPrefs.GetInt("SelectLevel") + 1);
                SceneManager.LoadScene("scene/gameScene");
            }
                
            else if ((PlayerPrefs.GetInt("SelectLevel") == 4))
            {
                PlayerPrefs.SetInt("SelectLevel", 4);
                SceneManager.LoadScene("scene/gameScene");
            }

            else
            {
                SceneManager.LoadScene("scene/TuoLuoYi");
            }
                
            //TODO
        }
        //进入Shop界面
        public void ShopCallBack()
        {
            Singleton<EndContextManager>.Instance.Push(new ShopContext());
        }
        //返回游戏初始界面
        public void BackCallBack()
        {
            Application.LoadLevel("Test");
        }
    }
}
