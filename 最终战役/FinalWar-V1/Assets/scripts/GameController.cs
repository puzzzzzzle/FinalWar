using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace artest.AIThirdPerson
{
    public class GameController : MonoBehaviour
    {
        //玩家信息
        private string player="1";
        private int level = 4;
        private int deFeng = 0;
        private int jinBi = 0;
        private int difficulty = 1;
        //属性方法
		public void addPOint(int deFeng, int jinBi){
			this.deFeng += deFeng;
			this.jinBi += jinBi;
		}
        public string getSscore()
        {
            return deFeng.ToString();
        }
        public string getMoney()
        {
            return jinBi.ToString();
        }
        //xml文件名
        private string result = "./weizhu.xml";

        //游戏物品
        public GameObject[] enemyPrefabs;
        public Transform[] positions;
        public GameLevel gameLevel;
        public Text time;
        private Transform cam;

        //需要初始化的类
        private XmlPlayerInformation xmlCenter;
        public PlayerInfromation p;

        // Use this for initialization 初始化
        void Start()
        {
            level = PlayerPrefs.GetInt("SelectLevel");
            difficulty = PlayerPrefs.GetInt("SelectDifficulty");
            player = PlayerPrefs.GetString("id");
            cam = GameObject.Find("ImageTarget-Image-cam1").transform;
            //test.text = "初始化开始！";
            xmlCenter = new XmlPlayerInformation();
            //关卡初始化

            //玩家不存在时新建，存在时加载
            if ((p = xmlCenter.selectUser(player)) == null)
            {
                p = new PlayerInfromation();
                p.id = player;
                p.money = 0;
                p.name = "play1";
                p.score = 0;
                p.level = 0;
                p.castle = 0;
                p.propone = 0;
                p.proptwo = 0;
                p.propthree = 0;
                xmlCenter.addUser(p);
                //test.text += p.id;
            }

            initPlayer(p);
            //确定关卡与难度
            if (level == 1)
            {
                gameLevel = new GameLevel1();
                if(difficulty == 1)
                {
                    gameLevel.gameTime = 5000;
                    gameLevel.maxEnemy = 6;
                }else if(difficulty == 2)
                {
                    gameLevel.gameTime = 6000;
                    gameLevel.maxEnemy = 9;
                }
                else
                {
                    gameLevel.gameTime = 7000;
                    gameLevel.maxEnemy = 12;
                }
                
            }else
			if (level == 2)
			{
				gameLevel = new GameLevel2();
                if (difficulty == 1)
                {
                    gameLevel.gameTime = 7000;
                    gameLevel.maxEnemy = 9;
                }
                else if (difficulty == 2)
                {
                    gameLevel.gameTime = 8000;
                    gameLevel.maxEnemy = 12;
                }
                else
                {
                    gameLevel.gameTime = 9000;
                    gameLevel.maxEnemy = 15;
                }
            }
            else
			if (level == 3)
			{
				gameLevel = new GameLevel3();
                gameLevel.gameTime = 0;
                if (difficulty == 1)
                {
                    gameLevel.maxEnemy = 10;
                }
                else if (difficulty == 2)
                {
                    gameLevel.maxEnemy = 12;
                }
                else
                {
                    gameLevel.maxEnemy = 15;
                }
            }
            else
            if(level == 4)
            {
                gameLevel = new GameLevel4();
                gameLevel.gameTime = 0;
                if (difficulty == 1)
                {
                    gameLevel.maxEnemy = 5;
                }
                else if (difficulty == 2)
                {
                    gameLevel.maxEnemy = 10;
                }
                else
                {
                    gameLevel.maxEnemy = 15;
                }
            }
            //else if(level == 5)
            //{
            //    PlayerPrefs.SetInt("SelectLevel", 5);
            //    PlayerPrefs.SetInt("SelectDifficulty", 2);
            //    SceneManager.LoadScene("scene/TuoLuoYi");
            //}

            gameLevel.currTime = 0;
            gameLevel.enemyPrefabs = enemyPrefabs;
            gameLevel.positions = positions;

        }

        private void gameFinal()
        {
            if (level == 1 || level == 2)
            {
                if (GameObject.Find("main").GetComponent<majorCity1>().blood <= 0)
                {
                    PlayerPrefs.SetString("score", deFeng.ToString());
                    PlayerPrefs.SetString("money", jinBi.ToString());
                    PlayerPrefs.SetString("level", level.ToString());
                    PlayerPrefs.SetString("result", "defeat");
                    SceneManager.LoadScene("root/end");
                }
                if (gameLevel.currTime >= gameLevel.gameTime)
                {
                    PlayerPrefs.SetString("score", deFeng.ToString());
                    PlayerPrefs.SetString("money", jinBi.ToString());
                    PlayerPrefs.SetString("level", level.ToString());
                    PlayerPrefs.SetString("result", "victory");
                    SceneManager.LoadScene("root/end");
                    p.money += jinBi;
                    p.score += deFeng;
                    //xmlCenter.changePlayer(p);
                }
            }
            else if (level == 3)
            {
                if (GameObject.Find("main").GetComponent<majorCity1>().blood <= 0)
                {
                    PlayerPrefs.SetString("score", deFeng.ToString());
                    PlayerPrefs.SetString("money", jinBi.ToString());
                    PlayerPrefs.SetString("level", level.ToString());
                    PlayerPrefs.SetString("result", "defeat");
                    SceneManager.LoadScene("root/end");
                }
                if (gameLevel.Boss.GetComponent<enemy>().blood <= 0)
                {
                    PlayerPrefs.SetString("score", deFeng.ToString());
                    PlayerPrefs.SetString("money", jinBi.ToString());
                    PlayerPrefs.SetString("level", level.ToString());
                    PlayerPrefs.SetString("result", "victory");
                    SceneManager.LoadScene("root/end");
                    p.money += jinBi;
                    p.score += deFeng;
                    //xmlCenter.changePlayer(p);
                }
            }
            else if (level == 4)
            {
                if (GameObject.Find("main").GetComponent<majorCity1>().blood <= 0)
                {
                    PlayerPrefs.SetString("score", deFeng.ToString());
                    PlayerPrefs.SetString("money", jinBi.ToString());
                    PlayerPrefs.SetString("level", level.ToString());
                    PlayerPrefs.SetString("player", player.ToString());
                    PlayerPrefs.SetString("result", "victory");
                    SceneManager.LoadScene("root/end");
                    p.money += jinBi;
                    p.score += deFeng;
                    //xmlCenter.changePlayer(p);
                }
            }
            else
            {
                PlayerPrefs.SetString("score", 0.ToString());
                PlayerPrefs.SetString("money", 0.ToString());
                PlayerPrefs.SetString("level", 0.ToString());
                PlayerPrefs.SetString("player", 0.ToString());
                PlayerPrefs.SetString("result", "victory");
                SceneManager.LoadScene("root/end");

            }
        }
        // Update is called once per frame  显示、判断逻辑
        void Update()
        {
            timeCenter();
            gameFinal();
        }
        //关卡物理逻辑，关卡时间逻辑
        private void FixedUpdate()
        {
            gameLevel.UpdateCalled(this.gameObject.transform,cam);
            gameLevel.currTime++;
        }
        //时间显示
        private void timeCenter()
        {
            if (level == 1|| level == 2)
            {
                time.text = "时间（秒）："+((int)( gameLevel.currTime / 50)).ToString() +"/"+ ((int)(gameLevel.gameTime / 50)).ToString();
            }
            else
            if (level == 3 || level == 4)
            {
                time.text = "时间（秒）：" + ((int)(gameLevel.currTime / 50)).ToString();
            }
            else
            {
                time.text = "时间（秒）：" + 0.ToString();
            }
        }
        private void initPlayer(PlayerInfromation p)
        {
            int[] inf = LevelToInformation.getCastle(p.castle);
            this.gameObject.GetComponent<majorCity1>().maxBlood = inf[4];
            this.gameObject.GetComponent<majorCity1>().blood = inf[4];
            this.gameObject.GetComponent<majorCity1>().maxLanliang = inf[5];
            this.gameObject.GetComponent<majorCity1>().labliang = inf[5];
            this.gameObject.GetComponent<majorCity1>().hujia = inf[2];
            this.gameObject.GetComponent<majorCity1>().mokang = inf[3];
            this.gameObject.GetComponent<TowerShoot>().fireRate = inf[6];
            this.gameObject.GetComponent<TowerShoot>().wuliDamage = inf[0];
            this.gameObject.GetComponent<TowerShoot>().mofaDamage = inf[1];
        }
    }
}
