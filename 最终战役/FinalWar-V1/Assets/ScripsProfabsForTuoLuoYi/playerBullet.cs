using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace artest.AIThirdPerson
{
    public class playerBullet : MonoBehaviour
    {
        public float damage;
        private void OnTriggerEnter(Collider other)
        {
            //Debug.Log(other.gameObject.tag);
            if (other.gameObject.tag.CompareTo("enemy") == 0)
            {
                //Debug.Log("enemy");
                other.gameObject.GetComponentInParent<EnemyForTuo>().damage(damage);
                Destroy(this.gameObject);
            }
            else if (other.gameObject.name.CompareTo("earth") == 0)
            {
                //Debug.Log("enemy");
                //other.gameObject.GetComponentInParent<EnemyForTuo>().damage(damage);
                Destroy(this.gameObject);
            }
        }
        private void OnCollisionEnter(Collision other)
        {
            //Debug.Log(other.gameObject.tag);
            if (other.gameObject.tag.CompareTo("enemy") == 0)
            {
                //Debug.Log("enemy");
                other.gameObject.GetComponentInParent<EnemyForTuo>().damage(damage);
                Destroy(this.gameObject);
            }
            else if (other.gameObject.name.CompareTo("earth") == 0)
            {
                //Debug.Log("enemy");
                //other.gameObject.GetComponentInParent<EnemyForTuo>().damage(damage);
                Destroy(this.gameObject);
            }
        }
    }
}
