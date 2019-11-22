using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace artest.AIThirdPerson
{
    public class ButtonChange : MonoBehaviour {

        public Sprite noraml;
        public Sprite change;
        public Text text;
        private Image image;
        public float time = 10;
        private float nextTime = 0;
        // Use this for initialization
        void Start() {
            image = this.gameObject.GetComponent<Image>();
            image.sprite = noraml;
            nextTime = Time.time;
        }

        // Update is called once per frame
        void Update() {

        }
        private void FixedUpdate()
        {
            if (Time.time < nextTime)
            {
                text.text = ((int)(nextTime-Time.time)).ToString();
            }
            else
            {
                image.sprite = noraml;
                text.text = "";
            }
        }
        public void changeImage()
        {
            int lanliang = 100;
            if (gameObject.GetComponent<PlayerShoot>() != null)
                lanliang = gameObject.GetComponent<PlayerShoot>().lanliang;
            else if (gameObject.GetComponent<BbuttonScripts>() != null)
                lanliang = (int)gameObject.GetComponent<BbuttonScripts>().lanlang;
            else if (gameObject.GetComponent<CbuttonLifeParit>() != null)
                lanliang = (int)gameObject.GetComponent<CbuttonLifeParit>().lanlang;
            else
                lanliang = 100;
                    

            if (Time.time >= nextTime && GameObject.Find("main").GetComponent<majorCity1>().labliang >= lanliang)
            {
                image.sprite = change;
                nextTime = Time.time + time;
                text.text = time.ToString();
                
            }

        }


}
}
