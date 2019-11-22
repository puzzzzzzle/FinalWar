using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace artest.AIThirdPerson
{
    public class XueLIangTiao : MonoBehaviour
    {
        public majorCity1 cityscrips;
        private Slider slider;
        private float Currvalue = 100;
        public Text xueliangtext;
        // Use this for initialization
        void Start()
        {
            slider = this.gameObject.GetComponent<Slider>();
            Currvalue = cityscrips.blood / cityscrips.maxBlood * 100;
            slider.value = Currvalue;
            xueliangtext.text = cityscrips.blood.ToString() + "/" + cityscrips.maxBlood.ToString();
        }

        // Update is called once per frame
        void Update()
        {
            //Debug.Log(slider.value);
            Currvalue = cityscrips.blood / cityscrips.maxBlood * 100;
            slider.value = Currvalue;
            xueliangtext.text = cityscrips.blood.ToString() + "/" + cityscrips.maxBlood.ToString();
        }
    }
}
