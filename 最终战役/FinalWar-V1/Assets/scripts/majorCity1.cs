using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace artest.AIThirdPerson
{
    public class majorCity1 : MonoBehaviour
    {

        public float maxBlood = 1000;
        public float blood = 1000;
        public float hujia = 1;
        public float maxLanliang = 1000;
        public float labliang = 1000;
        public float mokang = 1;

        public void wulidamage(float x)
        {
            blood-= (float)Math.Ceiling( 100 / (100+hujia)*x);
        }
        public void mofadamage(float x)
        {

            blood-= (float)Math.Ceiling(100 / (100 + mokang) * x);
        }
        public Transform getMainTransform()
        {
            return this.gameObject.transform;
        }
    }
}