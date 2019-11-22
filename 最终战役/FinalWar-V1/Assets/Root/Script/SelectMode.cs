using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace MoleMole
{
    public class ModeContext : BaseContext
    {
        public ModeContext()
            : base(UIType.Mode)
        {

        }
    }

    public class SelectMode: AnimateView
    {
  
        //记录鼠标点下的位置
        Vector2 down;

        //记录鼠标松开的位置
        Vector2 up;

        //是否点击AR模式按钮
        int isAR;
        //是否点击无尽模式按钮
        int isEndless;
        //是否点击陀螺仪模式按钮
        int isGryo;
        void Start()
        {
            isAR = 0;
            isAR = 0;
            isEndless = 0;
            down = new Vector2();
            up = new Vector2();
            // 1 不允许多点触屏
            Input.multiTouchEnabled = false;
        }
        public override void OnEnter(BaseContext context)
        {
            base.OnEnter(context);
        }

        public override void OnExit(BaseContext context)
        {
            DestroySelf();
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
        public void ButtonDown()
        {
#if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
       down = Input.touches[0].position;
#else

            down = Input.mousePosition;
#endif
        }
        //AR模式UP
        public void ARUp()
        {
            isAR = PlayerPrefs.GetInt("AR模式");
#if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
       up = Input.touches[0].position;
#else

            up = Input.mousePosition;
#endif
            if (Mathf.Abs(up.x - down.x) < 5.0&&isAR==1)
            {
                ARMode();
            }
        }
        //无尽模式UP
        public void EndLessUp()
        {
            isEndless = PlayerPrefs.GetInt("无尽模式");
#if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
       up = Input.touches[0].position;
#else

            up = Input.mousePosition;
#endif
            if (Mathf.Abs(up.x - down.x) < 5.0&&isEndless==1)
            {
                EndLess();
            }
        }
        //陀螺仪模式UP
        public void GyroUp()
        {
            isGryo = PlayerPrefs.GetInt("模拟VR");
#if !UNITY_EDITOR && (UNITY_IOS || UNITY_ANDROID)
       up = Input.touches[0].position;
#else

            up = Input.mousePosition;
#endif
            if (Mathf.Abs(up.x - down.x) < 5.0&&isGryo==1)
            {
                GyroMode();
            }
        }

        //AR模式
        public void ARMode()
        {
            Singleton<ContextManager>.Instance.Push(new SelectContext(),1.0);
        }
        //无尽模式
        public void EndLess()
        {
            PlayerPrefs.SetInt("SelectLevel", 4);
            PlayerPrefs.SetInt("SelectDifficulty", 2);
            SceneManager.LoadScene("Root/load");
        }
        //陀螺仪模式
        public void GyroMode()
        {
            PlayerPrefs.SetInt("SelectLevel", 5);
            PlayerPrefs.SetInt("SelectDifficulty", 2);
            SceneManager.LoadScene("Root/load");
        }
        //返回按钮
        public void BackButton()
        {
            Singleton<ContextManager>.Instance.Pop();

        }
    }
}
