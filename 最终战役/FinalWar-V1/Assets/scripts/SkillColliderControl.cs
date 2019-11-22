using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace artest.AIThirdPerson
{
    public class SkillColliderControl : MonoBehaviour
    {
        public float mofaDamageForTime;
        public float wuliDamageForTime;
        //public int timesForThisSkill;
        //Dictionary<enemy, int> enemysCollided;
        // Use this for initialization
        void Start()
        {
            //enemysCollided = new Dictionary<enemy, int>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag.CompareTo("enemy") == 0)
            {
                other.gameObject.GetComponent<enemy>().damageByMofa(mofaDamageForTime);
                other.gameObject.GetComponent<enemy>().damageByWuli(wuliDamageForTime);
                Debug.Log(other.gameObject.GetComponent<enemy>().blood);
            }
        }
        private void damageEnemy()
        {

        }
    }
}