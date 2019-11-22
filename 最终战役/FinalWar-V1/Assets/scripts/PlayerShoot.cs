using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace artest.AIThirdPerson
{
    public class PlayerShoot : MonoBehaviour
    {

        //声明发射点
        public Transform firepoint;
        //子弹速度
        public float fireSpeed;
        //声明子弹用的实例
        public PlayerButtle bullet;
        //初始化发射时间
        private float nextFire = 1F;
        //声明子弹间隔
        public float fireRate = 2F;
        //最远距离
        public int fireDistance = 5;
        public int wuliDamage = 10;
        public int mofaDamage = 10;
        public int lanliang = 10;
        //目标
        public GameObject ThisAudio;
        //public Transform mainTarget;
        // Use this for initialization
        Rigidbody clone;
        public void playershoot()
        {

            //点击左键并且时间已经大于间隔时间
            if (Time.time > nextFire )
            {
                PlayerInfromation p = GameObject.Find("main").GetComponent<GameController>().p;
                if (p != null&& this.gameObject.name.CompareTo("DButton") ==0)
                {
                    int[] inf = LevelToInformation.getLevel(p.level);
                    wuliDamage = inf[0];
                    mofaDamage = inf[1];
                    fireRate = inf[2];
                    shoot();
                }else if(p != null && this.gameObject.name.CompareTo("AButton") == 0 && GameObject.Find("main").GetComponent<majorCity1>().labliang >= lanliang)
                {
                    //Debug.Log("AAAAAAAAAAAAA");
                    int[] inf = LevelToInformation.getPropthree(p.propthree);
                    wuliDamage = inf[0];
                    mofaDamage = inf[1];
                    lanliang = inf[2];
                    fireRate = inf[3];
                    GameObject.Find("main").GetComponent<majorCity1>().labliang -= lanliang;
                    this.gameObject.GetComponent<ButtonChange>().time = fireRate;
                    shoot();
                    Destroy(Instantiate(ThisAudio, GameObject.Find("main").transform.position, Quaternion.identity), 5);
                }
                
            }
        }
        private void shoot()
        {
            //Debug.Log("fire", this);
            //更新间隔时间
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
            clone = bullet.shoot(temp, firepoint);
            clone.GetComponent<PlayerButtle>().wuliDamage = wuliDamage;
            clone.GetComponent<PlayerButtle>().mofaDamage = mofaDamage;
            //初始化子弹的方向速度
            clone.velocity = (target - firepoint.position) * fireSpeed;
            if (clone != null)
                Destroy(clone.gameObject, fireDistance);
        }
    }
}