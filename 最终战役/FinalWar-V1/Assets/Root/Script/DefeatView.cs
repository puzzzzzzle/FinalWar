using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace MoleMole
{
    public class DefeatContext : BaseContext
    {
        public DefeatContext()
            : base(UIType.Defeat)
        {

        }
    }

    public class DefeatView : AnimateView
    {
        //分数
        [SerializeField]
        public Text Score;
        //金钱
        [SerializeField]
        public Text Money;

        public override void OnEnter(BaseContext context)
        {
            PlayerPrefs.SetInt("Shop", 1);
            string score = PlayerPrefs.GetString("score");
            string money = PlayerPrefs.GetString("money");
            string player = PlayerPrefs.GetString("player");
            Score.text = score;
            Money.text = money;
            base.OnEnter(context);
        }

        public override void OnExit(BaseContext context)
        {
            base.OnExit(context);
        }

        public override void OnPause(BaseContext context)
        {
            base.OnPause(context);
        }

        public override void OnResume(BaseContext context)
        {
            base.OnResume(context);
        }
        //重新开始游戏
        public void RestartCallBack()
        {
            if ((PlayerPrefs.GetInt("SelectLevel") <= 4))
                SceneManager.LoadScene("scene/gameScene");
            else
                SceneManager.LoadScene("scene/TuoLuoYi");
            //TODO
        }
        //进入下一关
        public void NextCallBACK()
        {
            if (PlayerPrefs.GetInt("SelectLevel") <= 3)
            {
                PlayerPrefs.SetInt("SelectLevel", PlayerPrefs.GetInt("SelectLevel") + 1);
                SceneManager.LoadScene("scene/gameScene");
            }

            else if ((PlayerPrefs.GetInt("SelectLevel") == 4))
            {
                PlayerPrefs.SetInt("SelectLevel", 4);
                SceneManager.LoadScene("scene/gameScene");
            }

            else
            {
                SceneManager.LoadScene("scene/TuoLuoYi");
            }
            //TODO
        }
        //返回游戏初始界面
        public void BackCallBack()
        {
            Application.LoadLevel("Test");
        }

        //进入Shop界面
        public void ShopCallBack()
        {
            Singleton<EndContextManager>.Instance.Push(new ShopContext());
        }
    }
}
