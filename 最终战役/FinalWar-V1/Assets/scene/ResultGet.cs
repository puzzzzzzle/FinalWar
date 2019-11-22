using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultGet : MonoBehaviour {

    public Text text;
    string deFeng;
    string jinBi;
    // Use this for initialization
    void Start () {
        deFeng = PlayerPrefs.GetString("deFeng");
        jinBi = PlayerPrefs.GetString("jinBi");
        text.text = getText(deFeng, jinBi);
    }
	
	// Update is called once per frame
	void Update () {
        text.text = getText(deFeng, jinBi);
    }
    private string getText(string deFeng, string jinBi)
    {
        return "得分：" + deFeng + "    金币：" + jinBi;
    }
}
