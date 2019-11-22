using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace artest.AIThirdPerson
{
    public class Nengliangtiao : MonoBehaviour
    {
        public majorCity1 cityscrips;
        private Slider slider;
        private float Currvalue = 100;
        public Text lantiaotext;
        // Use this for initialization
        void Start()
        {
            slider = this.gameObject.GetComponent<Slider>();
            Currvalue = cityscrips.labliang / cityscrips.maxLanliang * 100;
            slider.value = Currvalue;
            lantiaotext.text = cityscrips.labliang.ToString() + "/" + cityscrips.maxLanliang.ToString();
        }

        // Update is called once per frame
        void Update()
        {
            //Debug.Log(slider.value);
            Currvalue = cityscrips.labliang / cityscrips.maxLanliang * 100;
            slider.value = Currvalue;
            lantiaotext.text = cityscrips.labliang.ToString() + "/" + cityscrips.maxLanliang.ToString();
        }
    }
}
