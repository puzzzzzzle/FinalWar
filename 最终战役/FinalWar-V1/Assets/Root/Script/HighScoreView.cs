using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Text;
using artest.AIThirdPerson;

namespace MoleMole
{
    public class HighScoreContext : BaseContext
    {
        public HighScoreContext()
            : base(UIType.HighScore)
        {

        }
    }

    public class HighScoreView : AnimateView
    {

        //定义访问JSP登录表单的get方式访问路径    获取数据
        //private string getUrl = "http://192.168.234.1:8080/GetRank.do?";
        //private string getUrl = "http://192.168.191.1:8080/GetRank.do?";
        private string getUrl = "http://47.93.195.255:8080/GetRank.do?";

        //显示当前账号的分数
        public string curScore;

        //定义访问JSP登录表单的get方式访问路径    获取数据
        //private string Url = "http://192.168.234.1:8080/GetData.do?";
        private string Url = "http://47.93.195.255:8080/GetData.do?";
        //
        //记录排行榜名次的Text
        public Text rankOne;
        public Text rankTwo;
        public Text rankThree;
        public Text rankFour;
        public Text rankFive;
        public Text rankSix;
        public Text rankSeven;
        public Text rankEight;
        public Text rankNine;
        public Text rankTen;

        //记录自己的名次
        public Text self;

        //当前玩家的姓名
        public string name;

        //联网玩家还是游客玩家
        string mode;
        public override void OnEnter(BaseContext context)
        {

            base.OnEnter(context);
        }

        void Start()
        {
            name = PlayerPrefs.GetString("player");
            mode = PlayerPrefs.GetString("temp");
            if (mode.Equals("default"))
            {
                //判断本地储存是否已经存在当前用户
                XmlPlayerInformation xml = new XmlPlayerInformation();
                PlayerInfromation local = xml.selectUser("default");
                if (local == null)
                {
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
                curScore = local.score.ToString();
            }
            else
            {
                string parameter = "";
                parameter += "UserName=" + name;
                StartCoroutine(getData(Url + parameter));

            }

            StartCoroutine(getRank(getUrl));
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
                curScore = dbData[1];

            }
        }

        //访问JSP服务器获得玩家数据
        IEnumerator getRank(string path)
        {
            WWW www = new WWW(path);
            yield return www;
            //如果发生错误，打印这个错误
            if (www.error != null)
            {
                nullInternet();
            }
            else
            {
                //如果服务器返回的是true

                //提取数据
                char[] chars = www.text.ToCharArray();
                List<string> dbPlayer = new List<string>();
                List<string> dbScore = new List<string>();
                StringBuilder data = new StringBuilder();
                int count = 0;
                for (int i = 0; i < chars.Length; i++)
                {
                    if (chars[i] != ';')
                    {
                        data.Append(chars[i]);
                    }
                    else
                    {
                        count++;
                        if ((count % 2)==0)
                        {
                            dbScore.Add(data.ToString());
                     
                        }
                        else
                        {
                        dbPlayer.Add(data.ToString());
                  
                        }
                        data.Length = 0;
                    }
                }
                int size = dbScore.Count;
                if (size == 0)
                {
                    nullData();
                }
                else if (size == 1)
                {
                    haveOne(dbPlayer, dbScore);
                }
                else if (size == 2)
                {
                    haveTwo(dbPlayer, dbScore);
                }
                else if (size == 3)
                {
                    haveThree(dbPlayer, dbScore);
                }
                else if (size == 4)
                {
                    haveFour(dbPlayer, dbScore);
                }
                else if (size == 5)
                {
                    haveFive(dbPlayer, dbScore);
                }
                else if (size == 6)
                {
                    haveSix(dbPlayer, dbScore);
                }
                else if (size == 7)
                {
                    haveSeven(dbPlayer, dbScore);
                }
                else if (size == 8)
                {
                    haveEight(dbPlayer, dbScore);
                }
                else if (size == 9)
                {
                    haveNine(dbPlayer, dbScore);
                }
                else if (size == 10)
                {
                    haveTen(dbPlayer, dbScore);
                }
            }
        }
        //排行榜没有数据
        public void nullInternet()
        {
            rankOne.text = "无法连接服务器";
            rankTwo.text = "无法连接服务器";
            rankThree.text = "无法连接服务器";
            rankFour.text = "无法连接服务器";
            rankFive.text = "无法连接服务器";
            rankSix.text = "无法连接服务器";
            rankSeven.text = "无法连接服务器";
            rankEight.text = "无法连接服务器";
            rankNine.text = "无法连接服务器";
            rankTen.text = "无法连接服务器";
            self.text = "无法连接服务器";
        }
        //排行榜没有数据
        public void nullData()
        {
            rankOne.text = "暂无数据";
            rankTwo.text = "暂无数据";
            rankThree.text = "暂无数据";
            rankFour.text = "暂无数据";
            rankFive.text = "暂无数据";
            rankSix.text = "暂无数据";
            rankSeven.text = "暂无数据";
            rankEight.text = "暂无数据";
            rankNine.text = "暂无数据";
            rankTen.text = "暂无数据";
        }

        //排行榜只有一名
        public void haveOne(List<string> dbPlayer,List<string> dbScore)
        {
            nullData();
            string s = "";
            s += "第一名  " + dbPlayer[0] + "\r\n";
            s += "分数     " + dbScore[0];
            rankOne.text = s;
            if (mode.Equals("default"))
            {
                self.text = "当前分数:" + curScore;
                return;
            }
            if (dbPlayer[0].Equals(name))
            {
                self.text = s;
            }
            else
            {
                self.text = "当前分数:" + curScore ;
            }
        }
        //排行榜只有两名
        public void haveTwo(List<string> dbPlayer, List<string> dbScore) { 
            haveOne(dbPlayer,dbScore);
            string s = "";
            s += "第二名  " + dbPlayer[1] + "\r\n";
            s += "分数     " + dbScore[1];
            rankTwo.text = s;

            if (mode.Equals("default"))
            {
                self.text = "当前分数:" + curScore;
                return;
            }
            if (dbPlayer[1].Equals(name))
            {
                self.text = s;
            }
            else
            {
                self.text = "当前分数:" + curScore ;
            }
        }
        //排行榜只有三名
        public void haveThree(List<string> dbPlayer, List<string> dbScore)
        {
            haveTwo(dbPlayer,dbScore);
            string s = "";
            s += "第三名  " + dbPlayer[2] + "\r\n";
            s += "分数     "+ dbScore[2];
            rankThree.text = s;

            if (mode.Equals("default"))
            {
                self.text = "当前分数:" + curScore;
                return;
            }
            if (dbPlayer[2].Equals(name))
            {
                self.text = s;
            }
            else
            {
                self.text = "当前分数:" + curScore ;
            }
        }
        //排行榜只四名
        public void haveFour(List<string> dbPlayer, List<string> dbScore)
        {
            haveThree(dbPlayer, dbScore);
            string s = "";
            s += "第四名  " + dbPlayer[3] + "\r\n";
            s += "分数     " + dbScore[3];
            rankFour.text = s;

            if (mode.Equals("default"))
            {
                self.text = "当前分数:" + curScore;
                return;
            }
            if (dbPlayer[3].Equals(name))
            {
                self.text = s;
            }
            else
            {
                self.text = "当前分数:" + curScore ;
            }
        }
        //排行榜只有五名
        public void haveFive(List<string> dbPlayer, List<string> dbScore)
        {
            haveFour(dbPlayer, dbScore);
            string s = "";
            s += "第五名  " + dbPlayer[4] + "\r\n";
            s += "分数     "  + dbScore[4];
            rankFive.text = s;
            if (mode.Equals("default"))
            {
                self.text = "当前分数:" + curScore;
                return;
            }
            if (dbPlayer[4].Equals(name))
            {
                self.text = s;
            }
            else
            {
                self.text = "当前分数:" + curScore;
            }
        }
        //排行榜只有六名
        public void haveSix(List<string> dbPlayer, List<string> dbScore)
        {
            haveFive(dbPlayer, dbScore);
            string s = "";
            s += "第六名  " + dbPlayer[5] + "\r\n";
            s += "分数     "  + dbScore[5];
            rankSix.text = s;
            if (mode.Equals("default"))
            {
                self.text = "当前分数:" + curScore;
                return;
            }
            if (dbPlayer[5].Equals(name))
            {
                self.text = s;
            }
            else
            {
                self.text = "当前分数:" + curScore ;
            }
        }
        //排行榜只有七名
        public void haveSeven(List<string> dbPlayer, List<string> dbScore)
        {
            haveSix(dbPlayer, dbScore);
            string s = "";
            s += "第七名  " + dbPlayer[6] + "\r\n";
            s += "分数     " + dbScore[6];
            rankSeven.text = s;
            if (mode.Equals("default"))
            {
                self.text = "当前分数:" + curScore;
                return;
            }
            if (dbPlayer[6].Equals(name))
            {
                self.text = s;
            }
            else
            {
                self.text = "当前分数:" + curScore;
            }
        }
        //排行榜只有八名
        public void haveEight(List<string> dbPlayer, List<string> dbScore)
        {
            haveSeven(dbPlayer, dbScore);
            string s = "";
            s += "第八名  " + dbPlayer[7] + "\r\n";
            s += "分数     " + dbScore[7];
            rankEight.text = s;
            if (mode.Equals("default"))
            {
                self.text = "当前分数:" + curScore;
                return;
            }
            if (dbPlayer[7].Equals(name))
            {
                self.text = s;
            }
            else
            {
                self.text = "当前分数:" + curScore ;
            }
        }
        //排行榜只有九名
        public void haveNine(List<string> dbPlayer, List<string> dbScore)
        {
            haveEight(dbPlayer, dbScore);
            string s = "";
            s += "第九名  " + dbPlayer[8] + "\r\n";
            s += "分数     "  + dbScore[8];
            rankNine.text = s;
            if (mode.Equals("default"))
            {
                self.text = "当前分数:" + curScore;
                return;
            }
            if (dbPlayer[8].Equals(name))
            {
                self.text = s;
            }
            else
            {
                self.text = "当前分数:" + curScore ;
            }
        }
        //排行榜只有十名
        public void haveTen(List<string> dbPlayer, List<string> dbScore)
        {
            haveNine(dbPlayer, dbScore);
            string s = "";
            s += "第二名:" + dbPlayer[9] + "\r\n";
            s += "分数   :"+ dbScore[9];
            rankTen.text = s;
            if (mode.Equals("default"))
            {
                self.text = "当前分数:" + curScore;
                return;
            }
            if (dbPlayer[9].Equals(name))
            {
                self.text = s;
            }
            else
            {
                self.text = "当前分数:" + curScore ;
            }
        }
        //返回OtherView
        //采用第二种Pop();
        public void BackCallBack()
        {
            Singleton<ContextManager>.Instance.Pop("第二种");
        }


    }
}
