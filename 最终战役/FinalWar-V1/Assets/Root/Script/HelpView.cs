using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace MoleMole
{
    public class HelpContext : BaseContext
    {
        public HelpContext()
            : base(UIType.Help)
        {

        }
    }

    public class HelpView : AnimateView
    {

       
        public override void OnEnter(BaseContext context)
        {
            base.OnEnter(context);
        }

        public override void OnExit(BaseContext context)
        {
            base.OnExit(context);
        }

        public void BackCallBack()
        {
            Singleton<ContextManager>.Instance.Pop();
        }
    }
}

