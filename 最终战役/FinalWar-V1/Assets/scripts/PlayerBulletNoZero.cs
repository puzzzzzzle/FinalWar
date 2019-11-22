using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace artest.AIThirdPerson
{
    public class PlayerBulletNoZero : PlayerButtle
    {
        protected new void fireDistory(Vector3 position, int zero)
        {
            if (destorypart != null)
            {
                //Debug.Log("播放动画");
                Instantiate(destorypart, new Vector3(position.x, position.y, position.z), Quaternion.identity, GameObject.Find("ImageTarget-Image-cam1").transform);
            }
            if (this.gameObject != null)
            {
                Destroy(this.gameObject, 0);
            }
        }
    }
}
