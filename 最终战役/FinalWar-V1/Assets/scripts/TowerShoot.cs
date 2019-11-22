using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace artest.AIThirdPerson
{
    public class TowerShoot : MonoBehaviour
    {

        //声明发射点
        public Transform firepoint;
        //子弹速度
        public float fireSpeed;
        //声明子弹用的实例
        public PlayerButtle bullet;
        //初始化发射时间
        private float nextFire = 1F;
        public int wuliDamage = 10;
        public int mofaDamage = 10;
        //敌人
        public List<GameObject> enemys;
        //声明子弹间隔
        public float fireRate = 2F;
        //最远距离
        public int fireDistance = 5;
        //目标
        //public Transform mainTarget;
        // Use this for initialization
        Rigidbody clone;
        public void playershoot()
        {
            enemys = GameObject.Find("main").GetComponent<GameController>().gameLevel.enemys;
            //enemys = gameObject.GetComponent<GameController>().gameLevel.enemys;
            //敌人数量不为零并且时间已经大于间隔时间
            //Debug.Log(enemys.Count);
            if (Time.time > nextFire && enemys != null && enemys.Count > 0)
            {
                if (this.gameObject.name.CompareTo("lol") == 0)
                {
                    PlayerInfromation p = GameObject.Find("main").GetComponent<GameController>().p;
                    if (p != null)
                    {
                        int[] inf = LevelToInformation.getDefence(p.defence);
                        wuliDamage = inf[0];
                        mofaDamage = inf[1];
                        fireRate = inf[2];
                    }
                }
                //Debug.Log("tower fire");
                //更新间隔时间
                nextFire = Time.time + fireRate;
                //获得目标点
                //Vector3 target = mainTarget.localPosition;
                // Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
                Vector3 target = new Vector3(0, 0, 0);
                float distance = 0;
                float currdistance = 0;
                foreach (GameObject curr in enemys)
                {
                    distance = getdistance(firepoint.position, curr.transform.position);
                    if (distance > currdistance)
                    {
                        currdistance = distance;
                        target = curr.transform.position;
                    }
                }
                //target = target + new Vector3(0, 0, 0);
                //firepoint.LookAt(target);
                //实例化子弹
                Vector3 temp = new Vector3(firepoint.position.x, firepoint.position.y + 0.5f, firepoint.position.z);
                //temp = new Vector3(temp.x, temp.y, temp.z);
                //temp.Set(temp.x , temp.y , temp.z );
                //clone = Instantiate(bullet, temp, firepoint.rotation, GameObject.Find("ImageTarget-Image-cam1").transform);
                Quaternion roation = Quaternion.identity;
                roation.SetFromToRotation(this.gameObject.transform.position, target);
                clone = bullet.shoot(temp, roation);
                clone.GetComponent<PlayerButtle>().wuliDamage = wuliDamage;
                clone.GetComponent<PlayerButtle>().mofaDamage = mofaDamage;
                //初始化子弹的方向速度
                clone.velocity = (target - firepoint.position) * fireSpeed;
                if (clone != null)
                    Destroy(clone.gameObject, fireDistance);
            }
        }
        private float getdistance(Vector3 from, Vector3 to)
        {
            return (float)Math.Sqrt((from.x - to.x) * (from.x - to.x) + (from.y - to.y) * (from.y - to.y) + (from.z - to.z) * (from.z - to.z));
        }
        //private void Start()
        //{
        //    //enemys = gameObject.GetComponent<GameController>().gameLevel.enemys;
        //}
        //private void Update()
        //{
        //    //enemys = gameObject.GetComponent<GameController>().gameLevel.enemys;
        //    playershoot();
        //}
        private void FixedUpdate()
        {
            playershoot();
        }
    }
}
