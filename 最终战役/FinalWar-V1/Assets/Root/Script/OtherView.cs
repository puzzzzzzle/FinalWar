using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace MoleMole
{
    public class OtherContext : BaseContext
    {
        public OtherContext() : base(UIType.Other)
        {

        }
    }

    public class OtherView : AnimateView
    {

        public override void OnEnter(BaseContext context)
        {
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
        //切换账号按钮
        public void SwitchAccount()
        {
            Application.LoadLevel("land");
        }
        //高分榜按钮
        //使用第二种Push
        public void HighScoreCallBack()
        {
            Singleton<ContextManager>.Instance.Push(new HighScoreContext(),2.0);
        }
        //帮助界面按钮
        public void HelpView()
        {
            Singleton<ContextManager>.Instance.Push(new HelpContext());
        }
        //退出界面按钮
        public void ExitCallBack()
        {
            Singleton<ContextManager>.Instance.Pop();
        }
    }
}

