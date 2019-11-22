using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.ThirdPerson;

namespace artest.AIThirdPerson
{
    public class EnemyForTuo : MonoBehaviour
    {
        public Slider bloodTiao;
        public Text xueliangtext;
        public float blood = 1000;
        public float maxBlood = 1000;
        public Rigidbody enemyBullet;
        public Transform[] positions;
        public Transform firePoint;
        public float shootOffset = 3;
        public int fireDistance = 20;
        public int fireSpeed = 20;
        private float nextTime;
        public float BulletDamage = 10;
        System.Random r ;
        public void damage(float d)
        {
            //Debug.Log("damage");
            blood -= d;
        }
        // Use this for initialization
        void Start()
        {
            r = new System.Random();
            nextTime = Time.time+shootOffset;
            Currvalue = (int)(blood / maxBlood * 100);
            bloodTiao.value = Currvalue;
            xueliangtext.text = ((int)blood).ToString() + "/" + ((int)maxBlood).ToString();
        }
        int Currvalue = 100;

        // Update is called once per frame
        void Update()
        {
            //Debug.Log(slider.value);
            Currvalue = (int)(blood / maxBlood * 100);
            bloodTiao.value = Currvalue;
            xueliangtext.text = ((int)blood).ToString() + "/" + ((int)maxBlood).ToString();
        }
        private void FixedUpdate()
        {
            shoot();
        }
        Rigidbody clone;
        private void shoot()
        {
            if (Time.time >= nextTime)
            {
                nextTime = Time.time + shootOffset;
                Vector3 target = positions[0].localPosition;
                //firepoint.LookAt(target);
                //实例化子弹
                //clone = Instantiate(bullet, temp, firepoint.rotation, GameObject.Find("ImageTarget-Image-cam1").transform);
                if (enemyBullet != null)
                {
                    clone = Instantiate(enemyBullet,firePoint.position,Quaternion.identity);
                    //初始化子弹的方向速度
                    clone.velocity = (target - firePoint.position) * fireSpeed;
                    clone.gameObject.GetComponent<EnemyBullet>().damage = BulletDamage;
                    //Animation a = gameObject.GetComponent<Animation>();
                    //if (a != null)
                    //{
                    //    gameObject.GetComponent<enemy>().pause();
                    //    a.CrossFade(attack);
                    //    gameObject.transform.LookAt(GameObject.Find("main").transform);
                    //    //Debug.Log("改变动画");
                    //    //a["Attack1"].wrapMode = WrapMode.Once;
                    //}
                }

                if (clone != null)
                    Destroy(clone.gameObject, fireDistance);
            }
        }
        
        int i = 0;
        private void moveRamdom()
        {
            if (positions[i] != null)
            {
                AICharacterControl ai = this.gameObject.GetComponent<AICharacterControl>();
                if (ai.agent.remainingDistance > ai.agent.stoppingDistance)
                {
                    //
                }
                else
                {
                    //Debug.Log("move dragon");
                    i = r.Next(0, positions.Length);
                    ai.target = positions[i];
                    //Debug.Log("move dragon" + i);
                }
            }
            else
            {
                i = r.Next(0, positions.Length);
                //Debug.Log("move dragon" + i);
            }
        }
    }
}