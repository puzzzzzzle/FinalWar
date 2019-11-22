using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace artest.AIThirdPerson
{
    public class MIaoZHunJingChange : MonoBehaviour
    {
        public Sprite noraml;
        public Sprite change;
        public Image image;
        public int time = 30;
        private int curr = 0;
        // Use this for initialization
        void Start()
        {
            image.sprite = noraml;
        }

        // Update is called once per frame
        void Update()
        {

        }
        private void FixedUpdate()
        {
            if (curr > 0)
            {
                curr--;
            }
            else
            {
                image.sprite = noraml;
                curr = 0;
            }
        }
        public void changeImage()
        {
            image.sprite = change;
            curr = time;
        }

    }

}
