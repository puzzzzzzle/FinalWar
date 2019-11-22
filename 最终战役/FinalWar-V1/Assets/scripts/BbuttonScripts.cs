using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace artest.AIThirdPerson
{
    public class BbuttonScripts : MonoBehaviour
    {


        public GameObject target;
        public GameObject storm;
        public Transform cam;
        public float offset = 10;
        private float nextTime = 0;
        public float lanlang = 50;
        public float damage = 1;
        public float damageTimes = 60;
        private float damageCurrTime = 0;
        private bool stat = false;
        
        public void OnClicked()
        {
            PlayerInfromation p = GameObject.Find("main").GetComponent<GameController>().p;
            if (p != null)
            {
                int[] inf = LevelToInformation.getProptwo(p.proptwo);
                damage = inf[0];
                lanlang = inf[1];
                offset = inf[2];
                this.gameObject.GetComponent<ButtonChange>().time = offset;
            }
            if (storm != null && Time.time > nextTime && GameObject.Find("main").GetComponent<majorCity1>().labliang >= lanlang)
            {
                nextTime = Time.time + offset;
                GameObject life = Instantiate(storm, target.transform.position + new Vector3(0,13,0), Quaternion.identity, cam);
                target.GetComponent<majorCity1>().labliang -= lanlang;
                Destroy(life, 8);
                stat = true;
            }
        }
        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        private void FixedUpdate()
        {
            if (stat)
            {
                if (damageCurrTime <= damageTimes)
                {
                    foreach (GameObject e in (target.GetComponent<GameController>().gameLevel.enemys))
                    {
                        e.GetComponent<enemy>().damageByMofa(damage);
                        //Debug.Log(e.GetComponent<enemy>().blood);
                    }
                    damageCurrTime++;
                }
                else
                {
                    stat = false;
                    damageCurrTime = 0;
                }
            }
        }
    }
}