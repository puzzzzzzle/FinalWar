using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace artest.AIThirdPerson
{
    public class PlayerSkill : PlayerButtle
    {
        public GameObject colliderForSkill;
        public int timeForSkill = 5;
        public int timesForThisSkill = 1;
        public GameObject part;
        //private void OnCollisionEnter(Collision collision)
        //{
        //    Debug.Log("col碰撞Enter:   " + collision.gameObject.name);
        //    if (collision.gameObject.tag.CompareTo("enemy") == 0)
        //    {

        //        fireDistory(gameObject.transform.position);
        //        collision.gameObject.GetComponent<enemy>().damageByWuli(wuliDamage);
        //        collision.gameObject.GetComponent<enemy>().damageByMofa(mofaDamage);
        //        Debug.Log("敌人血量:   " + collision.gameObject.GetComponent<enemy>().blood, this);
        //    }
        //    /*else
        //    {
        //        if (this != null)
        //        {
        //            fireDistory(gameObject.transform.localPosition);
        //        }
        //    }*/
        //}

        private void OnTriggerEnter(Collider collision)
        {
            Debug.Log("tri碰撞Enter:   " + collision.gameObject.name);
            fireDistory();
            //if (collision.gameObject.tag.CompareTo("enemy") == 0
            //    || collision.gameObject.name.CompareTo("earth") == 0)
            //{

                
            //    //collision.gameObject.GetComponent<enemy>().damageByWuli(wuliDamage);
            //    //collision.gameObject.GetComponent<enemy>().damageByMofa(mofaDamage);
            //    //Debug.Log("敌人血量:   " + collision.gameObject.GetComponent<enemy>().blood, this);
            //}
        }

        private void fireDistory()
        {
            if (destorypart != null&& colliderForSkill!=null)
            {
                //Debug.Log("播放动画");
                Instantiate(destorypart, this.gameObject.transform.position, Quaternion.identity, GameObject.Find("ImageTarget-Image-cam1").transform);
                // Debug.Log("碰撞体实例化");
                Vector3 temp = this.gameObject.transform.position - new Vector3(0, this.gameObject.transform.position.y, 0);
                GameObject colliderSkill = Instantiate(colliderForSkill, temp, Quaternion.identity, GameObject.Find("ImageTarget-Image-cam1").transform);
                colliderForSkill.GetComponent<SkillColliderControl>().wuliDamageForTime = wuliDamage;
                colliderForSkill.GetComponent<SkillColliderControl>().mofaDamageForTime = mofaDamage;
                if (part != null)
                {
                    Instantiate(part, temp, Quaternion.identity, GameObject.Find("ImageTarget-Image-cam1").transform);
                }
                Destroy(colliderSkill, timeForSkill);
                //Debug.Log("碰撞体实例化完成");
            }

            if (this.gameObject != null)
            {
                Destroy(this.gameObject, 0);
            }
        }
    }
}
