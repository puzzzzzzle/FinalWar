using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace artest.AIThirdPerson
{
    public class roate : MonoBehaviour
    {
        public int roateSped;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(0, 0, roateSped * Time.deltaTime, Space.Self);
        }
    }
}
