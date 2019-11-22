using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace artest.AIThirdPerson
{
    public class Player : MonoBehaviour
    {
        public EnemyForTuo dragon;
        public Slider bloodTiao;
        public Text xueliangtext;
        //声明发射点
        public Transform firepoint;
        //子弹速度
        public float fireSpeed = 5;
        //声明子弹用的实例
        public Rigidbody bullet;
        //初始化发射时间
        private float nextFire = 1F;
        //声明子弹间隔
        public float fireRate = 2F;
        //最远距离
        public int fireDistance = 5;
        public int bulletdamage = 10;
            //目标
            //public Transform mainTarget;
            // Use this for initialization
            Rigidbody clone;
        public void playershoot()
        {
            //点击左键并且时间已经大于间隔时间
            if (Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                //获得目标点
                //Vector3 target = mainTarget.localPosition;
                Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
                Vector3 target = ray.GetPoint(10);
                //firepoint.LookAt(target);
                //实例化子弹
                Vector3 temp = new Vector3(firepoint.position.x, firepoint.position.y, firepoint.position.z);
                //temp = new Vector3(temp.x, temp.y, temp.z);
                //temp.Set(temp.x , temp.y , temp.z );
                //clone = Instantiate(bullet, temp, firepoint.rotation, GameObject.Find("ImageTarget-Image-cam1").transform);
                clone = Instantiate(bullet, temp, Quaternion.identity);
                //初始化子弹的方向速度
                clone.velocity = (target - firepoint.position) * fireSpeed;
                clone.gameObject.GetComponent<playerBullet>().damage = bulletdamage;
                if (clone != null)
                    Destroy(clone.gameObject, fireDistance);
            }
        }


        public float maxBlood = 1000;
        public float blood = 1000;
        public void damage(float d)
        {
            blood -= d;
        }
        int Currvalue = 100;
        void Start()
        {
            Currvalue = (int)(blood / maxBlood * 100);
            bloodTiao.value = Currvalue;
            xueliangtext.text = ((int)blood).ToString() + "/" + ((int)maxBlood).ToString();
        }

        // Update is called once per frame
        void Update()
        {
            //Debug.Log(slider.value);
            Currvalue = (int)(blood / maxBlood * 100);
            bloodTiao.value = Currvalue;
            xueliangtext.text = ((int)blood).ToString() + "/" + ((int)maxBlood).ToString();
            if (blood <= 0)
            {
                PlayerPrefs.SetString("score", 0.ToString());
                PlayerPrefs.SetString("money", 0.ToString());
                PlayerPrefs.SetString("level", 5.ToString());
                //PlayerPrefs.SetString("player", PlayerPrefs.GetString("player"));
                PlayerPrefs.SetString("result", "defeat");
                Application.LoadLevel("root/end");
            }
            if (dragon.blood <= 0)
            {
                PlayerPrefs.SetString("score", 0.ToString());
                PlayerPrefs.SetString("money", 0.ToString());
                PlayerPrefs.SetString("level", 5.ToString());
                //PlayerPrefs.SetString("player", PlayerPrefs.GetString("player"));
                PlayerPrefs.SetString("result", "victory");
                Application.LoadLevel("root/end");
            }
        }

    }
}