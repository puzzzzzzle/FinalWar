using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


namespace MoleMole
{
    public class SelectContext : BaseContext
    {
        public SelectContext()
            : base(UIType.Select)
        {

        }
    }

    public class SelectView : AnimateView
    {
        public Dropdown ChooseDifficulty;
        //选择
        public int selectNum;
        public override void OnEnter(BaseContext context)
        {
            selectNum = 0;
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
        //返回OtherView
        //采用第二种Pop();

        //选择关卡1
        public void selectOne()
        {
            selectNum = 1;
        }
        //选择关卡2
        public void selectTwo()
        {
            selectNum = 2;
        }
        //选择关卡3
        public void selectThree()
        {
            selectNum = 3;
        }
        //开始游戏
        public void StartGame()
        {
            Debug.Log(selectNum);
            if (selectNum == 0)
            {
                PlayerPrefs.SetInt("SelectView", 0);
                Singleton<ContextManager>.Instance.Push(new PopContext(), "请选择关卡！");
                return;
            }
            int SelectDifficulty = 0;
            switch (ChooseDifficulty.value)
            {
                case 0:
                    SelectDifficulty = 1;
                    break;
                case 1:
                    //一般模式
                    SelectDifficulty = 2;
                    break;
                case 2:
                    //困难模式
                    SelectDifficulty = 3;
                    break;

            }
            //            Test

            PlayerPrefs.SetInt("SelectLevel", selectNum);
            PlayerPrefs.SetInt("SelectDifficulty", SelectDifficulty);
            Application.LoadLevel("Root/load");
        }
        //返回选择模式界面
        public void BackMode()
        {
            Singleton<ContextManager>.Instance.Pop("第二种");
        }

    }
}
