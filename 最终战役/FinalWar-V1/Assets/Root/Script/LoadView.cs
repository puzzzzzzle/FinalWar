
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace MoleMole
{
    public class LoadView : MonoBehaviour
    {

        public Slider process;
        public Text percentage;

        public void LoadGame()
        {
            StartCoroutine(StartLoading_1(1));
        }

        private IEnumerator StartLoading_1(int scene)
        {
            yield return new WaitForEndOfFrame();
            int displayProgress = 0;
            int toProgress = 0;
            //SceneManager.LoadScene("scene/TuoLuoYi");
            AsyncOperation op =null;
            if (PlayerPrefs.GetInt("SelectLevel") == 5)
            {
                op = Application.LoadLevelAsync("scene/TuoLuoYi");
            }
            else
            {
                op = Application.LoadLevelAsync("scene/gameScene");
            }
            
            op.allowSceneActivation = false;
            while (op.progress < 0.9f)
            {
                toProgress = (int)op.progress * 100;
                while (displayProgress < toProgress)
                {
                    ++displayProgress;
                    SetLoadingPercentage(displayProgress);
                    yield return new WaitForEndOfFrame();
                }
            }

            toProgress = 100;
            while (displayProgress < toProgress)
            {
                ++displayProgress;
                SetLoadingPercentage(displayProgress);
                yield return new WaitForEndOfFrame();
            }
            op.allowSceneActivation = true;
        }

        private void SetLoadingPercentage(float value)
        {
            process.value = value / 100;
            percentage.text = value + "%";
        }
        // Use this for initialization
        void Start()
        {
            LoadGame();

        }

        // Update is called once per frame
        void Update()
        {
        }
    }
}