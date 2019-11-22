using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace artest.AIThirdPerson
{
    public class Buttle : MonoBehaviour
    {

        public Rigidbody bullet;
        public GameObject destorypart;
        public float wuliDamage = 10;
        public float mofaDamage = 10;

        public majorCity1 m;

        // Use this for initialization
        void Start()
        {
            m = GameObject.Find("main").GetComponent<majorCity1>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnCollisionEnter(Collision collision)
        {
            //Debug.Log("col碰撞Enter:   " + collision.gameObject.name);
            if (collision.gameObject.name.CompareTo("main") == 0)
            {
                if (this != null)
                {
                    fireDistory(this.gameObject.transform.position);
                    if (m != null)
                    {
                        m.wulidamage(wuliDamage);
                        m.mofadamage(mofaDamage);
                    }
                    //Debug.Log("col碰撞Enter:   " + collision.gameObject.name);
                }

            }
            else if (collision.gameObject.tag.CompareTo("enemy") == 0)
            {

            }
            else
            {
                if (this != null)
                {
                    fireDistory(this.gameObject.transform.localPosition);
                }
            }
        }

        private void OnTriggerEnter(Collider collision)
        {
            //Debug.Log("tri碰撞Enter:   " + collision.gameObject.name);
            if (collision.gameObject.name.CompareTo("main") == 0)
            {
                if (this != null )
                {
                    fireDistory(this.gameObject.transform.position);
                    if (m != null)
                    {
                        m.wulidamage(wuliDamage);
                        m.mofadamage(mofaDamage);
                    }

                    //Debug.Log("col碰撞Enter:   " + collision.gameObject.name);
                }

            }
            else if (collision.gameObject.tag.CompareTo("enemy") == 0)
            {

            }
            else
            {
                if (this != null)
                {
                    //fireDistory(collision.transform.localPosition);
                }
            }
        }

        private void fireDistory(Vector3 position)
        {
            if (this.gameObject != null)
            {
                Destroy(this.gameObject, 0);
            }

            if (destorypart != null)
            {
                Instantiate(destorypart, position, Quaternion.identity, GameObject.Find("ImageTarget-Image-cam1").transform);
            }
        }


        public Rigidbody shoot(Vector3 temp, Quaternion firepoint)
        {

            return Instantiate(bullet, temp, firepoint, GameObject.Find("ImageTarget-Image-cam1").transform);
        }
    }
}