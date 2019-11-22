using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace artest.AIThirdPerson
{
    public class DeFeng : MonoBehaviour
    {

        public GameController enemys;
        public Text text;
        // Use this for initialization
        void Start()
        {
            //enemys = GameObject.Find("main").GetComponent<GameController>();
            text.text = getText();
        }

        // Update is called once per frame
        void Update()
        {
            text.text = getText();

        }
        private string getText()
        {
            return "得分：" + enemys.getSscore() + "    金币：" + enemys.getMoney();
        }
    }
}
