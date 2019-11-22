using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace artest.AIThirdPerson
{
    public class GunArcher : gun {
        public string getArrow = "Attack1";
        public string charge = "Attack1";
        public string aim = "Attack1";
        public string shoot = "Attack1";
        //public float time = 3;
        //private float shootTime = 0;
        private void FixedUpdate()
        {

            //点击左键并且时间已经大于间隔时间
            if (true && Time.time > nextFire)
            {
                //Debug.Log("fire", this);
                //更新间隔时间
                nextFire = Time.time + fireRate;
                //获得目标点
                Vector3 target = mainTarget.localPosition;
                //firepoint.LookAt(target);
                //实例化子弹
                Vector3 temp = firepoint.position;
                temp = new Vector3(temp.x + firepointOffset.x, temp.y + firepointOffset.y, temp.z + firepointOffset.z);
                temp.Set(temp.x + firepointOffset.x, temp.y + firepointOffset.y, temp.z + firepointOffset.z);
                //clone = Instantiate(bullet, temp, firepoint.rotation, GameObject.Find("ImageTarget-Image-cam1").transform);
                if (bullet != null)
                {
                    Quaternion roation = Quaternion.identity;
                    roation.SetFromToRotation(this.gameObject.transform.position, mainTarget.position);
                    clone = bullet.shoot(temp, roation);
                    //初始化子弹的方向速度
                    clone.velocity = (target - firepoint.position) * fireSpeed;
                    Animation a = gameObject.GetComponent<Animation>();
                    if (a != null)
                    {
                        gameObject.GetComponent<enemy>().pause();
                        a.CrossFade(getArrow);
                        a.PlayQueued(charge);
                        a.PlayQueued(aim);
                        a.PlayQueued(shoot);
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
