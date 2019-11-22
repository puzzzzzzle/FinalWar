using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace artest.AIThirdPerson
{
    public class TowerBullet : PlayerButtle
    {
        private void OnTriggerEnter(Collider collision)
        {
            //Debug.Log("tri碰撞Enter:   " + collision.gameObject.name);
            if (collision.gameObject.tag.CompareTo("enemy") == 0)
            {

                fireDistory(collision.gameObject.transform.position,0);
                collision.gameObject.GetComponent<enemy>().damageByWuli(wuliDamage);
                collision.gameObject.GetComponent<enemy>().damageByMofa(mofaDamage);
                //Debug.Log("敌人血量:   " + collision.gameObject.GetComponent<enemy>().blood, this);
            }
            //else if (collision.gameObject.name.CompareTo("earth") == 0)
            //{
            //    fireDistory(gameObject.transform.position);
            //}
        }
    }
}