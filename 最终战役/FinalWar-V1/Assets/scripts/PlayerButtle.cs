using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace artest.AIThirdPerson
{
    public class PlayerButtle : MonoBehaviour
    {
        public int offset = 0;
        public Rigidbody bullet;
        public GameObject destorypart;
        public float wuliDamage = 10;
        public float mofaDamage = 10;


        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnCollisionEnter(Collision collision)
        {
            //Debug.Log("col碰撞Enter:   " + collision.gameObject.name);
            if (collision.gameObject.tag.CompareTo("enemy") == 0)
            {
                
                fireDistory(collision.gameObject.transform.position, 0);
                collision.gameObject.GetComponent<enemy>().damageByWuli(wuliDamage);
                collision.gameObject.GetComponent<enemy>().damageByMofa(mofaDamage);
                //Debug.Log("敌人血量:   " + collision.gameObject.GetComponent<enemy>().blood, this);
            }
            /*else
            {
                if (this != null)
                {
                    fireDistory(gameObject.transform.localPosition);
                }
            }*/
        }

        private void OnTriggerEnter(Collider collision)
        {
            //Debug.Log("tri碰撞Enter:   " + collision.gameObject.name);
            if (collision.gameObject.tag.CompareTo("enemy") == 0)
            {

                fireDistory(collision.gameObject.transform.position , 0);
                collision.gameObject.GetComponent<enemy>().damageByWuli(wuliDamage);
                collision.gameObject.GetComponent<enemy>().damageByMofa(mofaDamage);
                //Debug.Log("敌人血量:   " + collision.gameObject.GetComponent<enemy>().blood, this);
            }
            else if (collision.gameObject.name.CompareTo("earth") == 0
                || collision.gameObject.name.CompareTo("main") == 0)
            {
                fireDistory(gameObject.transform.position , 0);
            }
        }

        protected void fireDistory(Vector3 position,int zero)
        {
            if (destorypart != null)
            {
                //Debug.Log("播放动画");
                Instantiate(destorypart, new Vector3(position.x, zero, position.z), Quaternion.identity, GameObject.Find("ImageTarget-Image-cam1").transform);
            }
            if (this.gameObject != null)
            {
                Destroy(this.gameObject, 0);
            }
        }


        public Rigidbody shoot(Vector3 temp, Transform firepoint)
        {
            return Instantiate(bullet, temp, firepoint.rotation, GameObject.Find("ImageTarget-Image-cam1").transform);
        }

        internal Rigidbody shoot(Vector3 temp, Quaternion rotation)
        {
            return Instantiate(bullet, temp, rotation, GameObject.Find("ImageTarget-Image-cam1").transform);
        }

        public Rigidbody shoot(Vector3 temp, Transform firepoint,GameObject parent)
        {
            return Instantiate(bullet, temp, firepoint.rotation, parent.transform);
        }
    }
}