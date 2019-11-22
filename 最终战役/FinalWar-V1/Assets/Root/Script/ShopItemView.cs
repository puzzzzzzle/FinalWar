using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using artest.AIThirdPerson;
namespace MoleMole
{
    public class ShopItemContext : BaseContext
    {
        public ShopItemContext() : base(UIType.ShopItem)
        {

        }
    }

    public class ShopItemView: AnimateView
    {
        //介绍面板的文字内容
        [SerializeField]
        public Text ButtonText;
        //按钮上的文字内容
        [SerializeField]
        public Text oneLeft;
        [SerializeField]
        public Text oneRight;
        [SerializeField]
        public Text Introduce;
        [SerializeField]
        public Text NeedMoney;
        [SerializeField]
        //显示金币数
        public Text money;
        int TagControl;

        //定义访问JSP登录表单的get方式访问路径    获取数据
        //private string getUrl = "http://192.168.234.1:8080/GetData.do?";
        private string getUrl = "http://47.93.195.255:8080/GetData.do?";

        //定义访问JSP登录表单的get方式访问路径    上传最新数据
        //private string subUrl = "http://192.168.234.1:8080/SaveData.do?";
        private string subUrl = "http://47.93.195.255:8080/SaveData.do?";
        //tag 控制显示 0：人物升级；1：城堡升级；2:防御塔升级 ；3：道具一升级；4：道具二升级；5：道具五升级

        //判断本地储存是否已经存在当前用户
        XmlPlayerInformation xml ;
        PlayerInfromation local;

        public void OnEnter(BaseContext context,int tag)
        {
            ////向服务器传递的参数
            //string parameter = "";
            //parameter += "UserName=" + user;
            ////开始传递
            //StartCoroutine(login(Url + parameter));
            base.OnEnter(context);
            TagControl = tag;
            if(local != null)
            {
                switch (tag)
                {
                    case 0:
                        changeCharater(local.level);
                        break;
                    case 1:
                        changeCasetle(local.castle);
                        break;
                    case 2:
                        changeDefence(local.defence);
                        break;
                    case 3:
                        changeOne(local.propone);
                        break;

                    case 4:
                        changeTwo(local.proptwo);
                        break;
                    case 5:
                        changeThree(local.propthree);
                        break;

                }
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


        void Start()
        {
            xml = new XmlPlayerInformation();
            string mode = PlayerPrefs.GetString("temp");
            if (mode.Equals("default"))
            {
                local = xml.selectUser("default");
                if(local == null)
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
                money.text = local.money.ToString();
                int tag = PlayerPrefs.GetInt("Tag");
                switch (tag)
                {
                    case 0:
                        changeCharater(local.level);
                        break;
                    case 1:
                        changeCasetle(local.castle);
                        break;
                    case 2:
                        changeDefence(local.defence);
                        break;
                    case 3:
                        changeOne(local.propone);
                        break;

                    case 4:
                        changeTwo(local.proptwo);
                        break;
                    case 5:
                        changeThree(local.propthree);
                        break;

                }
            }
            else
            {
                string user = PlayerPrefs.GetString("player");
                string id = user + "id";
                local = xml.selectUser(id);
                string parameter = "";
                if (local == null)
                {
                    parameter += "UserName=" + user;
                    //创建新的本地记录
                    StartCoroutine(getData(getUrl + parameter));

                }
                money.text = local.money.ToString();
                int tag = PlayerPrefs.GetInt("Tag");
                switch (tag)
                {
                    case 0:
                        changeCharater(local.level);
                        break;
                    case 1:
                        changeCasetle(local.castle);
                        break;
                    case 2:
                        changeDefence(local.defence);
                        break;
                    case 3:
                        changeOne(local.propone);
                        break;

                    case 4:
                        changeTwo(local.proptwo);
                        break;
                    case 5:
                        changeThree(local.propthree);
                        break;

                }
                string score = local.score.ToString();
                string m = local.money.ToString();
                string level = local.level.ToString();
                string castle = local.castle.ToString();
                string defence = local.defence.ToString();
                string propone = local.propone.ToString();
                string proptwo = local.proptwo.ToString();
                string propthree = local.propthree.ToString();
                parameter += "UserName=" + user + "&";
                parameter += "Score=" + score + "&";
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
        //改变金钱和等级
        //change = 0 人物等级提升 =1 城堡提升 =2 防御塔提升 =3 道具1数加1 =4 道具2数加1 = 5道具3数加1 

        public void changeMoney(string user,float m,int change)
        {
            string mode = PlayerPrefs.GetString("temp");

            switch (change)
            {
                case 0:local.level += 1;
                    changeCharater(local.level);
                    break;
                case 1:local.castle += 1;
                    changeCasetle(local.castle);
                    break; 
                case 2:local.defence += 1;
                    changeDefence(local.defence);
                    break;
                case 3:local.propone += 1;
                    changeOne(local.propone);
                    break;
                case 4:local.proptwo += 1;
                    changeTwo(local.proptwo);
                    break;
                case 5:local.propthree += 1;
                    changeThree(local.propthree);
                    break;
            }
            local.money += m;
            money.text = local.money.ToString();
            xml.changePlayer(local);
            if (mode.Equals("land"))
            {
                string parameter = "";
                string score = local.score.ToString();
                string mon = local.money.ToString();
                string level = local.level.ToString();
                string castle = local.castle.ToString();
                string defence = local.defence.ToString();
                string propone = local.propone.ToString();
                string proptwo = local.proptwo.ToString();
                string propthree = local.propthree.ToString();
                parameter += "UserName=" + user + "&";
                parameter += "Score=" + score + "&";
                parameter += "Money=" + mon + "&";
                parameter += "Level=" + level + "&";
                parameter += "Castle=" + castle + "&";
                parameter += "Defence=" + defence + "&";
                parameter += "Propone=" + propone + "&";
                parameter += "Proptwo=" + proptwo + "&";
                parameter += "Propthree=" + propthree;
                StartCoroutine(subData(subUrl + parameter));
            }
        }

        //改变人物升级面板
        public void changeCharater(int lv)
        {
            string introduce = "";
            int before;
            int after;
            string up;
            int needMoney;
            oneLeft.text = "人物等级";
            oneRight.text = "LV" + lv;
            introduce += "物理攻击 ";
            //升级前
            before = LevelToInformation.getLevel(lv) [0];
            //升级后
            after = LevelToInformation.getLevel(lv+1)[0];
            up = " " + before + "-->" + after;
            introduce += up+"\r\n";
            introduce += "魔法攻击 ";
            //升级前
            before = LevelToInformation.getLevel(lv)[1];
            //升级后
            after = LevelToInformation.getLevel(lv + 1)[1];
            up = " " + before + "-->" + after;
            introduce += up + "\r\n";
            introduce += "普攻冷却 ";
            //升级前
            before = LevelToInformation.getLevel(lv)[2];
            //升级后
            after = LevelToInformation.getLevel(lv + 1)[2];
            up = " " + before + "-->" + after;
            introduce += up;
            Introduce.text = introduce;
            needMoney = 3300 + lv * 1500;
            NeedMoney.text = needMoney.ToString();

            ButtonText.text = "升级";
        }
        //改变城堡升级面板
        public void changeCasetle(int lv)
        {
            string introduce = "";
            int before;
            int after;
            string up;
            int needMoney;

            oneLeft.text = "城堡等级";
            oneRight.text = "LV" + lv;
            //升级前
            before = LevelToInformation.getCastle(lv)[0];
            //升级后
            after = LevelToInformation.getCastle(lv+1)[0];
            up = " " + before + "-->" + after;
            introduce += "物理攻击 ";
            introduce += up + "\r\n";
            //升级前
            before = LevelToInformation.getCastle(lv)[1];
            //升级后
            after = LevelToInformation.getCastle(lv + 1)[1];
            up = " " + before + "-->" + after;
            introduce += "魔法攻击 ";
            introduce += up + "\r\n";
            //升级前
            before = LevelToInformation.getCastle(lv)[2];
            //升级后
            after = LevelToInformation.getCastle(lv + 1)[2];
            up = " " + before + "-->" + after;
            introduce += "护甲值 ";
            introduce += up + "\r\n";
            //升级前
            before = LevelToInformation.getCastle(lv)[3];
            //升级后
            after = LevelToInformation.getCastle(lv + 1)[3];
            up = " " + before + "-->" + after;
            introduce += "魔抗值 ";
            introduce += up + "\r\n";
            //升级前
            before = LevelToInformation.getCastle(lv)[4];
            //升级后
            after = LevelToInformation.getCastle(lv + 1)[4];
            up = " " + before + "-->" + after;
            introduce += "血量值 ";
            introduce += up + "\r\n";
            //升级前
            before = LevelToInformation.getCastle(lv)[5];
            //升级后
            after = LevelToInformation.getCastle(lv + 1)[5];
            up = " " + before + "-->" + after;
            introduce += "蓝量值 ";
            introduce += up + "\r\n";
            //升级前
            before = LevelToInformation.getCastle(lv)[6];
            //升级后
            after = LevelToInformation.getCastle(lv + 1)[6];
            up = " " + before + "-->" + after;
            introduce += "攻击速度 ";
            introduce += up;
            Introduce.text = introduce;
            needMoney = 3000 + 2000 * lv;
            NeedMoney.text = needMoney.ToString();
            ButtonText.text = "升级";
        }

        //改变防御塔升级面板
        public void changeDefence(int lv)
        {
            string introduce = "";
            int before;
            int after;
            string up;
            int needMoney;
            oneLeft.text = "防御塔等级";
            oneRight.text = "LV" + lv;
            //升级前
            before = LevelToInformation.getDefence(lv)[0];
            //升级后
            after = LevelToInformation.getDefence(lv + 1)[0];
            up = " " + before + "-->" + after;
            introduce += "物理攻击 ";
            introduce += up + "\r\n";
            //升级前
            before = LevelToInformation.getDefence(lv)[1];
            //升级后
            after = LevelToInformation.getDefence(lv + 1)[1];
            up = " " + before + "-->" + after;
            introduce += "魔法攻击 ";
            introduce += up + "\r\n";
            //升级前
            before = LevelToInformation.getDefence(lv)[2];
            //升级后
            after = LevelToInformation.getDefence(lv + 1)[2];
            up = " " + before + "-->" + after;
            introduce += "攻击速度 ";       
            introduce += up;
            Introduce.text = introduce;
            needMoney = 3150 + 1200 * lv;
            NeedMoney.text = needMoney.ToString();
            ButtonText.text = "升级";
        }

        //改变道具1购买界面
        public void  changeOne(int lv)
        {
            string introduce = "";
            int before;
            int after;
            string up;
            int needMoney;
            //升级前
            before = LevelToInformation.getPropone(lv)[0];
            //升级后
            after = LevelToInformation.getPropone(lv + 1)[0];
            up = " " + before + "-->" + after;
            introduce += "回血量 ";
            introduce += up + "\r\n";
            //升级前
            before = LevelToInformation.getPropone(lv)[1];
            //升级后
            after = LevelToInformation.getPropone(lv + 1)[1];
            up = " " + before +"s"+ "-->" + after+"s";
            introduce += "持续时间 ";
            introduce += up + "\r\n";
            //升级前
            before = LevelToInformation.getPropone(lv)[2];
            //升级后
            after = LevelToInformation.getPropone(lv + 1)[2];
            up = " " + before + "-->" + after;
            introduce += "耗蓝量 ";
            introduce += up + "\r\n";
            //升级前
            before = LevelToInformation.getPropone(lv)[3];
            //升级后
            after = LevelToInformation.getPropone(lv + 1)[3];
            up = " " + before + "-->" + after;
            introduce += "冷却时间 ";
            introduce += up;
            oneLeft.text = "治愈之光";
            oneRight.text ="Lv" +lv.ToString();
            Introduce.text = introduce;
            needMoney = 4000 + 2000 * lv;
            NeedMoney.text = needMoney.ToString();
            ButtonText.text = "升级";
        }

        //改变道具2购买界面
        public void changeTwo(int lv)
        {
            string introduce = "";
            int before;
            int after;
            string up;
            int needMoney;
            //升级前
            before = LevelToInformation.getProptwo(lv)[0];
            //升级后
            after = LevelToInformation.getProptwo(lv + 1)[0];
            up = " " + before + "-->" + after;
            introduce += "魔法攻击 ";
            introduce += up + "\r\n";
            //升级前
            before = LevelToInformation.getProptwo(lv)[1];
            //升级后
            after = LevelToInformation.getProptwo(lv + 1)[1];
            up = " " + before + "-->" + after;
            introduce += "耗蓝量 ";
            introduce += up + "\r\n";
            //升级前
            before = LevelToInformation.getProptwo(lv)[2];
            //升级后
            after = LevelToInformation.getProptwo(lv + 1)[2];
            up = " " + before + "-->" + after;
            introduce += "冷却时间 ";
            introduce += up;
            oneLeft.text = "流星火雨";
            oneRight.text = "Lv" + lv.ToString();
            Introduce.text = introduce;
            needMoney = 4000 + 2000 * lv;
            NeedMoney.text = needMoney.ToString();
            ButtonText.text = "升级";
        }
        //改变道具3购买界面
        public void changeThree(int lv)
        {
            string introduce = "";
            int before;
            int after;
            string up;
            int needMoney;
            //升级前
            before = LevelToInformation.getPropthree(lv)[0];
            //升级后
            after = LevelToInformation.getPropthree(lv + 1)[0];
            up = " " + before + "-->" + after;
            introduce += "物理攻击 ";
            introduce += up + "\r\n";
            //升级前
            before = LevelToInformation.getPropthree(lv)[1];
            //升级后
            after = LevelToInformation.getPropthree(lv + 1)[1];
            up = " " + before + "-->" + after;
            introduce += "魔法攻击 ";
            introduce += up + "\r\n";
            //升级前
            before = LevelToInformation.getPropthree(lv)[2];
            //升级后
            after = LevelToInformation.getPropthree(lv + 1)[2];
            up = " " + before + "-->" + after;
            introduce += "耗蓝量 ";
            introduce += up + "\r\n";
            //升级前
            before = LevelToInformation.getPropthree(lv)[3];
            //升级后
            after = LevelToInformation.getPropthree(lv + 1)[3];
            up = " " + before + "-->" + after;
            introduce += "冷却时间 ";
            introduce += up;
            oneLeft.text = "能量爆破";
            oneRight.text = "Lv" + lv.ToString();
            Introduce.text = introduce;
            needMoney = 4000 + 2000 * lv;
            NeedMoney.text = needMoney.ToString();
            ButtonText.text = "升级";
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
 
        //升级或购买按钮
        public void UpgradeOrBuyButton()
        {
            UpgradOrBuy(TagControl);
        }

        //升级或购买
        public void UpgradOrBuy(int tag)
        {
            string user = PlayerPrefs.GetString("player");
            int needMoney;
            int shop = PlayerPrefs.GetInt("Shop");
            switch (tag)
            {
                case 0:
                    needMoney = 3300 + local.level * 1500;
                    if (local.money < needMoney)
                    {
                        PlayerPrefs.SetInt("SelectView", 2);
                        if (shop == 0)
                        {
                            Singleton<ContextManager>.Instance.Push(new PopContext(), "金币不够！");
                        }
                        else if (shop == 1)
                        {
                            Singleton<EndContextManager>.Instance.Push(new PopContext(), "金币不够！");
                        }
                        return;
                    }
                    if (local.level >= 2)
                    {
                        PlayerPrefs.SetInt("SelectView", 2);
                        if (shop == 0)
                        {
                            Singleton<ContextManager>.Instance.Push(new PopContext(), "人物等级已满！");
                        }
                        else if (shop == 1)
                        {
                            Singleton<EndContextManager>.Instance.Push(new PopContext(), "人物等级已满！");
                        }
                        return;
                    }
                    money.text = (local.money-needMoney).ToString();
                    changeMoney(user, -needMoney,0);
                    break;
                case 1:
                    needMoney = 3000 + 2000 * local.castle;
                    if (local.money < needMoney)
                    {
                        PlayerPrefs.SetInt("SelectView", 2);
                        if (shop == 0)
                        {
                            Singleton<ContextManager>.Instance.Push(new PopContext(), "金币不够！");
                        }
                        else if (shop == 1)
                        {
                            Singleton<EndContextManager>.Instance.Push(new PopContext(), "金币不够！");
                        }
                        return;
                    }
                    if (local.castle >= 2)
                    {
                            PlayerPrefs.SetInt("SelectView", 2);
                        if (shop == 0)
                        {
                            Singleton<ContextManager>.Instance.Push(new PopContext(), "城堡等级已满！");
                        }
                        else if (shop == 1)
                        {
                            Singleton<EndContextManager>.Instance.Push(new PopContext(), "城堡等级已满！");
                        }
                        return;
                    }
                    money.text = (local.money - needMoney).ToString();
                    changeMoney(user, -needMoney,1);
                    break;
                case 2:
                    needMoney = 3150 + 1200 * local.defence;
                    if (local.money < needMoney)
                    {
                        PlayerPrefs.SetInt("SelectView", 2);
                        if (shop == 0)
                        {
                            Singleton<ContextManager>.Instance.Push(new PopContext(), "金币不够！");
                        }
                        else if (shop == 1)
                        {
                            Singleton<EndContextManager>.Instance.Push(new PopContext(), "金币不够！");
                        }
                        return;
                    }
                    if (local.defence >= 2)
                    {
                        PlayerPrefs.SetInt("SelectView", 2);
                        if (shop == 0)
                        {
                            Singleton<ContextManager>.Instance.Push(new PopContext(), "防御塔等级已满！");
                        }
                        else if (shop == 1)
                        {
                            Singleton<EndContextManager>.Instance.Push(new PopContext(), "防御塔等级已满！");
                        }
                        return;
                    }
                    money.text = (local.money - needMoney).ToString();
                    changeMoney(user, -needMoney,2);
                    break;
                case 3:
                    needMoney = 4000 + 2000 * local.propone;
                    if (local.money < needMoney)
                    {
                        PlayerPrefs.SetInt("SelectView", 2);
                        if (shop == 0)
                        {
                            Singleton<ContextManager>.Instance.Push(new PopContext(), "金币不够！");
                        }
                        else if (shop == 1)
                        {
                            Singleton<EndContextManager>.Instance.Push(new PopContext(), "金币不够！");
                        }
                        return;
                    }
                    if (local.propone >= 2) 
                    {
                        PlayerPrefs.SetInt("SelectView", 2);
                        if (shop == 0)
                        {
                            Singleton<ContextManager>.Instance.Push(new PopContext(), "技能等级已满！");
                        }
                        else if (shop == 1)
                        {
                            Singleton<EndContextManager>.Instance.Push(new PopContext(), "技能等级已满！");
                        }
                        return;
                    }
                    money.text = (local.money - needMoney).ToString();
                    changeMoney(user, -needMoney,3);

                    break;
                case 4:
                    needMoney = 4000 + 2000 * local.proptwo;
                    if (local.money < needMoney)
                    {
                        PlayerPrefs.SetInt("SelectView", 2);
                        if (shop == 0)
                        {
                            Singleton<ContextManager>.Instance.Push(new PopContext(), "金币不够！");
                        }
                        else if (shop == 1)
                        {
                            Singleton<EndContextManager>.Instance.Push(new PopContext(), "金币不够！");
                        }
                        return;
                    }
                    if (local.proptwo >= 2)
                    {
                        PlayerPrefs.SetInt("SelectView", 2);
                        if (shop == 0)
                        {
                            Singleton<ContextManager>.Instance.Push(new PopContext(), "技能等级已满！");
                        }
                        else if (shop == 1)
                        {
                            Singleton<EndContextManager>.Instance.Push(new PopContext(), "技能等级已满！");
                        }
                        return;
                    }
                    money.text = (local.money - needMoney).ToString();
                    changeMoney(user, -needMoney,4);
                    break;
                case 5:
                    needMoney = 4000 + 2000 * local.propthree;
                    if (local.money < needMoney)
                    {
                        PlayerPrefs.SetInt("SelectView", 2);
                        if (shop == 0)
                        {
                            Singleton<ContextManager>.Instance.Push(new PopContext(), "金币不够！");
                        }
                        else if (shop == 1)
                        {
                            Singleton<EndContextManager>.Instance.Push(new PopContext(), "金币不够！");
                        }
                        return;
                    }
                    if (local.propthree >= 2)
                    {
                        PlayerPrefs.SetInt("SelectView", 2);
                        if (shop == 0)
                        {
                            Singleton<ContextManager>.Instance.Push(new PopContext(), "技能等级已满！");
                        }
                        else if (shop == 1)
                        {
                            Singleton<EndContextManager>.Instance.Push(new PopContext(), "技能等级已满！");
                        }
                        return;
                    }
                    money.text = (local.money - needMoney).ToString();
                    changeMoney(user, -needMoney,5);

                    break;

            }
        }

    }
}

