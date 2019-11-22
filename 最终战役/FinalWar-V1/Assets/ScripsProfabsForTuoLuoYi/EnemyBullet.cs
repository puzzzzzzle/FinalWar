using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace artest.AIThirdPerson
{
    public class EnemyBullet : MonoBehaviour
    {
        public float damage = 10;
        private void OnTriggerEnter(Collider other)
        {
            //Debug.Log(other.gameObject.name);
            if (other.gameObject.name.CompareTo("player") == 0)
            {
                //Debug.Log("player");
                other.gameObject.GetComponent<Player>().damage(damage);
                Destroy(this.gameObject);
            }
        }
        private void OnCollisionEnter(Collision other)
        {
            //Debug.Log(other.gameObject.name);
            if (other.gameObject.name.CompareTo("player") == 0)
            {
                //Debug.Log("player");
                other.gameObject.GetComponent<Player>().damage(damage);
                Destroy(this.gameObject);
            }
        }
    }
}
