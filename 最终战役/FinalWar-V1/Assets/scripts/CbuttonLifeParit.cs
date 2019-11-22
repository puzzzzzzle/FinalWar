using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace artest.AIThirdPerson
{
    public class CbuttonLifeParit : MonoBehaviour {
        public GameObject target;
        public GameObject lifeStream;
        public Transform cam;
        public int addBlood = 10;
        public float offset = 100;
        private float nextTime = 0;
        public float lanlang = 50;
        public int times = 100;
        private bool stat = false;
        private int currTimes = 0;
        public void OnClicked()
        {
            if (lifeStream != null && Time.time > nextTime && GameObject.Find("main").GetComponent<majorCity1>().labliang >= lanlang)
            {
                stat = true;
                nextTime = Time.time + offset;
                GameObject life = Instantiate(lifeStream, target.transform.position, Quaternion.identity, cam);
                target.GetComponent<majorCity1>().labliang -= lanlang;
                Destroy(life, 8);
            }
        }
        private void FixedUpdate()
        {
            if (stat)
            {
                if(currTimes <= times)
                {
                    currTimes++;
                    if (target.GetComponent<majorCity1>().blood + addBlood <= target.GetComponent<majorCity1>().maxBlood)
                    {
                        target.GetComponent<majorCity1>().blood += addBlood;
                    }
                    else
                    {
                        target.GetComponent<majorCity1>().blood = target.GetComponent<majorCity1>().maxBlood;
                    }
                }
                else
                {
                    currTimes = 0;
                    stat = false;
                }
            }
        }
    }
}
