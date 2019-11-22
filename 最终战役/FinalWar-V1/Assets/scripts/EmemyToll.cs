using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace artest.AIThirdPerson
{
    public class EmemyToll : enemy
    {
        public float distance = 10;
        bool stat = true;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name.CompareTo("main") == 0)
            {
                this.gameObject.GetComponent<gun>().currRate = this.gameObject.GetComponent<gun>().fireRate;
                if (stat)
                {
                    this.gameObject.GetComponent<gun>().nextFire = Time.time;
                    stat = false;
                }
                //
                target = new Transform[]
{
                    this.gameObject.transform
};
            }
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.name.CompareTo("main") == 0)
            {
                this.gameObject.GetComponent<gun>().currRate = this.gameObject.GetComponent<gun>().fireRate;
                if (stat)
                {
                    this.gameObject.GetComponent<gun>().nextFire = Time.time;
                    stat = false;
                }
                //this.gameObject.GetComponent<gun>().nextFire = Time.time;
                target = new Transform[]
                {
                    this.gameObject.transform
                };
            }
        }
        private void Update()
        {
            gameObject.transform.LookAt(GameObject.Find("main").transform);
            if (blood <= 0)
            {
                if (this.gameObject != null)
                {
                    enemyDestory();
                }

            }
            if (this.gameObject.GetComponent<gun>() == null)
            {
                Destroy(this.gameObject);
            }else if(this.gameObject.GetComponent<gun>().mainTarget ==null)
            {
                this.gameObject.GetComponent<gun>().mainTarget = GameObject.Find("main").transform;
            }
            else
            if (getdistance(this.gameObject.transform.position, this.gameObject.GetComponent<gun>().mainTarget.position) > distance)
            {
                target = new Transform[]
    {
                                    this.gameObject.GetComponent<gun>().mainTarget
    };
            }
        }
        private float getdistance(Vector3 from, Vector3 to)
        {
            return (float)Math.Sqrt((from.x - to.x) * (from.x - to.x) + (from.y - to.y) * (from.y - to.y) + (from.z - to.z) * (from.z - to.z));
        }
        //        private void OnCollisionExit(Collision collision)
        //        {
        //            target = new Transform[]
        //{
        //                    GameObject.Find("main").transform
        //};
        //        }
        //        private void OnTriggerExit(Collider other)
        //        {
        //            target = new Transform[]
        //{
        //                    GameObject.Find("main").transform
        //};
        //        }

    }
}