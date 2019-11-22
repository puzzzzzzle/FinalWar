using UnityEngine;
using System.Collections;

namespace artest.AIThirdPerson
{
    public class MoveController : MonoBehaviour
    {
        bool jump = false;
        public int height = 5;
        public int flyHeight = 300;
        bool fly = false;
        public int speed = 5;
        public int flyPeriod = 100;
        public float offsetFroFly = 1000000f;
        public Transform earthPlayer;
        public void onAnimatorMove(Vector2 vec)
        {
            //Debug.Log("move:"+vec.x.ToString()+":"+vec.y.ToString());
            if (vec.x != 0 || vec.y != 0)
            {
                //设置角色的朝向（朝向当前坐标+摇杆偏移量）  
                //transform.LookAt(new Vector3(transform.position.x + vec.x, transform.position.y, transform.position.z + vec.y));
                //移动玩家的位置（按朝向位置移动）  
                transform.Translate(new Vector3(vec.x, 0, vec.y) * Time.deltaTime * speed);
                //if (!jump && !fly)
                //{
                //    this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, 1.5f, this.gameObject.transform.position.z);
                //}
                //播放奔跑动画  
                //animation.CrossFade("run");
            }
        }
        int offset = 0;
        public void onJump()
        {
            //if (this.gameObject.transform.position.y == 1.5)
            //{
            //    jump = false;
            //    high = true;
            //}
            if (fly && this.gameObject.transform.position.y < flyHeight)
            {
                jump = true;
                transform.Translate(new Vector3(0, 5, 0) * Time.deltaTime * 5);
            }
            else
            if (offset < height)
            {
                jump = false;
                transform.Translate(new Vector3(0, 5, 0) * Time.deltaTime * 5);
                offset++;
            }
        }
        public void flyStatus()
        {

            if (!fly)
            {
                this.gameObject.GetComponent<Rigidbody>().useGravity = false;
                fly = true;
                speed = 20;
            }
            else
            {
                this.gameObject.GetComponent<Rigidbody>().useGravity = true;
                fly = false;
                speed = 5;
            }
        }
        private void OnCollisionEnter(Collision collision)
        {
            //Debug.Log(collision.gameObject.name);
            if (collision.gameObject.name.CompareTo("earth") == 0)
            {
                jump = false;
                offset = 0;
            }
        }
        private void OnCollisionStay(Collision collision)
        {
            //Debug.Log(collision.gameObject.name);
            if (collision.gameObject.name.CompareTo("earth") == 0)
            {
                jump = false;
                offset = 0;
            }
        }
        int flyPeriodStatus = 50;
        bool flyPeriodStatusBool = true;
        private void FixedUpdate()
        {
            
            if (fly)
            {
                if (flyPeriodStatusBool)
                {
                    flyPeriodStatus++;
                    if (flyPeriodStatus >= 100)
                    {
                        flyPeriodStatusBool = false;
                    }
                }
                else
                {
                    flyPeriodStatus--;
                    if (flyPeriodStatus <= 0)
                    {
                        flyPeriodStatusBool = true;
                    }
                }

                this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + ((flyPeriodStatus - 50) / offsetFroFly), this.gameObject.transform.position.z);
                
                //earthPlayer.transform.position = new Vector3(this.gameObject.transform.position.x, 0, this.gameObject.transform.position.z);


                //Debug.Log(this.gameObject.transform.position.y);
                //Debug.Log((flyPeriodStatus - 50) / offsetFroFly);
            }
            earthPlayer.transform.position = new Vector3(this.gameObject.transform.position.x, 0, this.gameObject.transform.position.z);

        }

    }
}
