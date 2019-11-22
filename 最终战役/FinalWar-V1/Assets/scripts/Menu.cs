using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour {

    public GameObject[] buttons;
	// Use this for initialization
	void Start () {
        //if (PlayerPrefs.GetInt("level") == 5)
        //{
        //    PlayerPrefs.SetInt("SelectLevel", 5);
        //    PlayerPrefs.SetInt("SelectDifficulty", 2);
        //    SceneManager.LoadScene("scene/TuoLuoYi");
        //}
        buttonControl(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void pause()
    {
        //Time.timeScale = 0;
        //临时把暂停回复放在一起
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            buttonControl(true);
        }
        else
        {
            Time.timeScale = 1;
            buttonControl(false);
        }
    }
    public void resume()
    {
        Time.timeScale = 1;
        buttonControl(false);
    }
    private void buttonControl(bool stat)
    {
        foreach (GameObject b in buttons)
        {
            b.gameObject.SetActive(stat);
        }
    }
    public void exit()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Test");
    }
}
