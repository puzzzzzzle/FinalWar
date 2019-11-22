using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


namespace MoleMole
{
    public class MainMenuContext : BaseContext
    {
        public MainMenuContext() : base(UIType.MainMenu)
        {

        }
    }

    public class MainMenuView : AnimateView
    {

        public override void OnEnter(BaseContext context)
        {
            PlayerPrefs.SetInt("Shop", 0);
            base.OnEnter(context);
        }

        public override void OnExit(BaseContext context)
        {
            base.OnExit(context);
        }

        public override void OnPause(BaseContext context)
        {
            _animator.SetTrigger("OnExit");
        }

        public override void OnResume(BaseContext context)
        {
            _animator.SetTrigger("OnEnter");
        }

        public void otherCallBack()
        {
            Singleton<ContextManager>.Instance.Push(new OtherContext());
        }

        public void ShopCallBack()
        {
            Singleton<ContextManager>.Instance.Push(new ShopContext());
        }

        public void NewGameCallBack()
        {
            Singleton<ContextManager>.Instance.Push(new ModeContext());
        }

        public void ExitCallBack()
        {
            Application.Quit();
        }
    }
}
