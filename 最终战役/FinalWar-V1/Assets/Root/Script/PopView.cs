using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


namespace MoleMole
{
    public class PopContext : BaseContext
    {
        public PopContext() : base(UIType.Pop)
        {

        }
    }

    public class PopView : AnimateView
    {

        [SerializeField]
        private Button _buttonSure;
        [SerializeField]
        private Text text;
        public void OnEnter(BaseContext context,string message)
        {
            base.OnEnter(context);
            text.text = message;
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

        public void OptionCallBack()
        {
            int select = PlayerPrefs.GetInt("SelectView");
            int shop = PlayerPrefs.GetInt("Shop");
            if (shop == 1)
            {
                Singleton<EndContextManager>.Instance.Pop(1);
                return;
            }
            if (select == 0)
            {
                Singleton<ContextManager>.Instance.Pop();
            }
            else if(select == 1)
            {
                Singleton<LandContextManager>.Instance.Pop();
            }else if(select == 2)
            {
                Singleton<ContextManager>.Instance.Pop(1);
            }
        }

    }
}
