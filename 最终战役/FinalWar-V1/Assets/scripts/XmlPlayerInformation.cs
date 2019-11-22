using System.IO;
using System;
using System.Xml;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace artest.AIThirdPerson
{
    public class XmlPlayerInformation
    {
        private string PIN = "afbhask66f6awef.l;,;,<><?>k;ef;;fbsfhkajfhashfiawueaefhiaefh564ef6aefsaesf4";
        XmlDocument doc;
        private MD5 md5;
        string path = "./test.xml";
        //Text test;
        public XmlPlayerInformation()
        {
            md5 = MD5.Create();
            if (SystemInfo.operatingSystem.Contains("Windows"))
            {
                path = Application.dataPath + "//players.xml";
            }
            else
            if (SystemInfo.operatingSystem.Contains("Android"))
            {
                path = Application.persistentDataPath + "//players.xml";
            }
            if (SystemInfo.operatingSystem.Contains("IOS"))
            {
                path = Application.persistentDataPath + "//players.xml";
            }
            doc = new XmlDocument();

            if (File.Exists(path))
            {
                doc.Load(path);
            }
            else
            {
                initXml();
            }
            

        }
        /*public XmlPlayerInformation(Text t)
        {

            test = t;
            if (SystemInfo.operatingSystem.Contains("Windows"))
            {
                test.text += "windows";
                path = Application.dataPath + "//players.xml";
            }
            else if (SystemInfo.operatingSystem.Contains("Android"))
            {
                test.text += "android";
                path = Application.persistentDataPath + "//players.xml";
            }
            md5 = MD5.Create();
            doc = new XmlDocument();
            if (File.Exists(path))
            {
                doc.Load(path);
            }
            else
            {
                initXml();
            }
        }*/
        public void initXml()
        {
            //test.text += "开始创建文件！";
            FileStream f = File.Create(path);
            f.Close();
            //test.text += "创建文件完成！";
            doc = new XmlDocument();
            //说明节点
            XmlDeclaration xdDec = doc.CreateXmlDeclaration("1.0", "utf-8", null);
            doc.AppendChild(xdDec);
            //创建根节点  
            XmlElement xeRoot = doc.CreateElement("players");
             //给节点属性赋值
            xeRoot.SetAttribute("version", "1.0");
            xeRoot.SetAttribute("PIN", "");
            doc.AppendChild(xeRoot);
            //Console.WriteLine(xeRoot.ToString());
            doc.Save(path);
            updateHash();
        }
        public bool addUser(PlayerInfromation player)
        {
            if (!check())
            {
                return false;
            }
            XmlNode root = doc.DocumentElement;
            if (selectUser(player.id) != null)
            {
                return false;
            }
            //玩家根节点
            XmlElement xelKey = doc.CreateElement("player");
            XmlAttribute xelId = doc.CreateAttribute("id");
            xelId.InnerText = player.id;
            xelKey.SetAttributeNode(xelId);
            XmlAttribute xeltime = doc.CreateAttribute("time");
            xeltime.InnerText = System.DateTime.Now.ToLongDateString();
            xelKey.SetAttributeNode(xeltime);
            //玩家姓名
            XmlElement xelname = doc.CreateElement("name");
            xelname.InnerText = player.name;
            xelKey.AppendChild(xelname);
            //玩家金币
            XmlElement xelmoney = doc.CreateElement("money");
            xelmoney.InnerText = player.money.ToString();
            xelKey.AppendChild(xelmoney);
            //玩家成绩
            XmlElement xelscore = doc.CreateElement("score");
            xelscore.InnerText = player.score.ToString();
            xelKey.AppendChild(xelscore);
            //玩家level
            XmlElement xellevel = doc.CreateElement("level");
            xellevel.InnerText = player.level.ToString();
            xelKey.AppendChild(xellevel);
            //玩家castle
            XmlElement xelcastle = doc.CreateElement("castle");
            xelcastle.InnerText = player.castle.ToString();
            xelKey.AppendChild(xelcastle);
            //玩家defence
            XmlElement xeldefence = doc.CreateElement("defence");
            xeldefence.InnerText = player.defence.ToString();
            xelKey.AppendChild(xeldefence);
            //玩家propone
            XmlElement xelpropone = doc.CreateElement("propone");
            xelpropone.InnerText = player.propone.ToString();
            xelKey.AppendChild(xelpropone);
            //玩家proptwo
            XmlElement xelproptwo = doc.CreateElement("proptwo");
            xelproptwo.InnerText = player.proptwo.ToString();
            xelKey.AppendChild(xelproptwo);
            //玩家propthree
            XmlElement xelpropthree = doc.CreateElement("propthree");
            xelpropthree.InnerText = player.propthree.ToString();
            xelKey.AppendChild(xelpropthree);

            root.AppendChild(xelKey);
            doc.Save(path);
            updateHash();
            return true;
        }
        public PlayerInfromation selectUser(string id)
        {
            if (!check())
            {
                return null;
            }
            XmlNode root = doc.DocumentElement;
            if (root.ChildNodes == null)
            {
                return null;
            }
            XmlNodeList allPlayer = root.ChildNodes;
            PlayerInfromation playerreturn = null;

            
            foreach (XmlNode player in allPlayer)
            {
                XmlElement temp = (XmlElement)player;
                //Console.WriteLine(temp.GetAttribute("id"));
                if (temp.GetAttribute("id").CompareTo(id) == 0)
                {
                   
                    playerreturn = new PlayerInfromation();
                    playerreturn.id = temp.GetAttribute("id");
                    playerreturn.time = temp.GetAttribute("time");
                    XmlNodeList information = player.ChildNodes;
                    //informations of player
                    playerreturn.name = information.Item(0).InnerText;
                    playerreturn.money = double.Parse(information.Item(1).InnerText);
                    playerreturn.score = double.Parse(information.Item(2).InnerText);
                    playerreturn.level = int.Parse(information.Item(3).InnerText);
                    playerreturn.castle = int.Parse(information.Item(4).InnerText);
                    playerreturn.defence = int.Parse(information.Item(5).InnerText);
                    playerreturn.propone = int.Parse(information.Item(6).InnerText);
                    playerreturn.proptwo = int.Parse(information.Item(7).InnerText);
                    playerreturn.propthree = int.Parse(information.Item(8).InnerText);
                    return playerreturn;
                }
            }
            return playerreturn;
        }
        public bool deletePlayer(string id)
        {
            if (!check())
            {
                return false;
            }
            if (selectUser(id) == null)
            {
                return false;
            }
             XmlElement xe = doc.DocumentElement; // DocumentElement 获取xml文档对象的根XmlElement.
            string strPath = string.Format("/players/player[@id=\"{0}\"]",id);
            XmlElement selectXe = (XmlElement)xe.SelectSingleNode(strPath);  //selectSingleNode 根据XPath表达式,获得符合条件的第一个节点.
            selectXe.ParentNode.RemoveChild(selectXe);
            doc.Save(path);
            updateHash();
            return true;
        }
        public bool changePlayer(PlayerInfromation player)
        {
            if (!check())
            {
                return false;
            }
            if (selectUser(player.id) == null)
            {
                return false;
            }
            XmlElement xe = doc.DocumentElement; // DocumentElement 获取xml文档对象的根XmlElement.
            string strPath = string.Format("/players/player[@id=\"{0}\"]", player.id);
             XmlElement selectXe = (XmlElement)xe.SelectSingleNode(strPath);  //selectSingleNode 根据XPath表达式,获得符合条件的第一个节点.
            selectXe.SetAttribute("time", System.DateTime.Now.ToLongDateString());//也可以通过SetAttribute来增加一个属性
            selectXe.GetElementsByTagName("money").Item(0).InnerText = player.money.ToString();
            selectXe.GetElementsByTagName("score").Item(0).InnerText = player.score.ToString();
            selectXe.GetElementsByTagName("name").Item(0).InnerText = player.name.ToString();
            selectXe.GetElementsByTagName("level").Item(0).InnerText = player.level.ToString();
            selectXe.GetElementsByTagName("castle").Item(0).InnerText = player.castle.ToString();
            selectXe.GetElementsByTagName("defence").Item(0).InnerText = player.defence.ToString();
            selectXe.GetElementsByTagName("propone").Item(0).InnerText = player.propone.ToString();
            selectXe.GetElementsByTagName("proptwo").Item(0).InnerText = player.proptwo.ToString();
            selectXe.GetElementsByTagName("propthree").Item(0).InnerText = player.propthree.ToString();
            //test.text += "开始存储！";
            doc.Save(path);
            //test.text += "存储完成！";
            updateHash();
            return true;
        }
        private string GetMD5HashString(Encoding encode, string sourceStr)
        {
            StringBuilder sb = new StringBuilder();

            byte[] source = md5.ComputeHash(encode.GetBytes(sourceStr));
            for (int i = 0; i < source.Length; i++)
            {
                sb.Append(source[i].ToString("x2"));
            }
            return sb.ToString();
        }
        private string gethash()
        {
            FileInfo f = new FileInfo(path);
            string tempPath = f.DirectoryName;
            //Console.WriteLine(tempPath);
            XmlNode root = doc.DocumentElement;
            string players = getElementString(root);
            //Console.WriteLine("str:" + players);
            return GetMD5HashString(Encoding.UTF8, tempPath+players + PIN + SystemInfo.deviceUniqueIdentifier+ SystemInfo.graphicsDeviceVendorID.ToString());
        }
        private string getElementString(XmlNode e)
        {
            StringBuilder sb = new StringBuilder();
            if (e.ChildNodes == null)
            {
                return "";
            }
            XmlNodeList allPlayer = e.ChildNodes;
            foreach (XmlNode player in allPlayer)
            {
                XmlElement temp = (XmlElement)player;
                sb.Append(temp.GetAttribute("id"));
                sb.Append(temp.GetAttribute("time"));
                XmlNodeList information = player.ChildNodes;
                sb.Append(information.Item(0).InnerText);
                sb.Append(information.Item(1).InnerText);
                sb.Append(information.Item(2).InnerText);
                sb.Append(information.Item(3).InnerText);
                sb.Append(information.Item(4).InnerText);
                sb.Append(information.Item(5).InnerText);
                sb.Append(information.Item(6).InnerText);
                sb.Append(information.Item(7).InnerText);
                sb.Append(information.Item(8).InnerText);
            }
            return sb.ToString();
        }
        private void updateHash()
        {
            XmlElement root = doc.DocumentElement;
            root.SetAttribute("PIN", gethash());
            doc.Save(path);
        }
        private bool check()
        {
            XmlElement root = doc.DocumentElement;
            //Debug.Log("new:  " + gethash());
            //Debug.Log("ori:   " + root.GetAttribute("PIN"));
            if (gethash().CompareTo(root.GetAttribute("PIN"))==0){
                //test.text += "检测通过";
                return true;
            }
            else
            {
                File.Delete(path);
                initXml();
                return false;
            }
        }
    }
}
