using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace artest.AIThirdPerson
{
    public class gun : MonoBehaviour
    {
        //声明发射点
        public Transform firepoint;
        //距离人物的差值
        public Vector3 firepointOffset;
        //子弹速度
        public float fireSpeed;
        //声明子弹用的实例
        public Buttle bullet;
        //初始化发射时间
        public float nextFire = 1F;
        //声明子弹间隔
        public float fireRate = 2F;
        public float currRate = 1000000f;
        //最远距离
        public int fireDistance = 5;
        //目标
        public Transform mainTarget;

        public string attack = "Attack1";


        protected Rigidbody clone;
        private void Start()
        {
            nextFire = Time.time + currRate;
        }
        private void FixedUpdate()
        {

            //时间已经大于间隔时间
            if (Time.time > nextFire)
            {
                //Debug.Log("fire", this);
                //更新间隔时间
                nextFire = Time.time + currRate;
                //获得目标点
                Vector3 target = mainTarget.localPosition;
                //firepoint.LookAt(target);
                //实例化子弹
                Vector3 temp = firepoint.position;
                temp = new Vector3(temp.x + firepointOffset.x, temp.y + firepointOffset.y, temp.z + firepointOffset.z);
                temp.Set(temp.x + firepointOffset.x, temp.y + firepointOffset.y, temp.z + firepointOffset.z);
                //clone = Instantiate(bullet, temp, firepoint.rotation, GameObject.Find("ImageTarget-Image-cam1").transform);
                if(bullet != null)
                {
                    clone = bullet.shoot(temp, firepoint.rotation);
                    //初始化子弹的方向速度
                    clone.velocity = (target - firepoint.position) * fireSpeed;
                    Animation a = gameObject.GetComponent<Animation>();
                    if (a != null)
                    {
                        gameObject.GetComponent<enemy>().pause();
                        a.CrossFade(attack);
                        gameObject.transform.LookAt(GameObject.Find("main").transform);
                        //Debug.Log("改变动画");
                        //a["Attack1"].wrapMode = WrapMode.Once;
                    }
                }
               
                if (clone != null)
                    Destroy(clone.gameObject, fireDistance);
            }
        }
    }
}