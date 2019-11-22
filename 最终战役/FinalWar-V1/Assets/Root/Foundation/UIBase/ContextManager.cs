using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 *	
 *  Manage Context For UI Stack
 *
 *	by Xuanyi
 *
 */

namespace MoleMole
{
    public class ContextManager
    {
        private Stack<BaseContext> _contextStack = new Stack<BaseContext>();

        private ContextManager()
        {
            Push(new MainMenuContext());
        }

        public void Push(BaseContext nextContext)
        {

            if (_contextStack.Count != 0)
            {
                BaseContext curContext = _contextStack.Peek();
                BaseView curView = Singleton<UIManager>.Instance.GetSingleUI(curContext.ViewType).GetComponent<BaseView>();
                curView.OnPause(curContext);
            }

            _contextStack.Push(nextContext);
            BaseView nextView = Singleton<UIManager>.Instance.GetSingleUI(nextContext.ViewType).GetComponent<BaseView>();
            nextView.OnEnter(nextContext);
        }

        public void Pop()
        {
            if (_contextStack.Count != 0)
            {
                BaseContext curContext = _contextStack.Peek();
                _contextStack.Pop();
                                                                                                                                                        
                BaseView curView = Singleton<UIManager>.Instance.GetSingleUI(curContext.ViewType).GetComponent<BaseView>();
                curView.OnExit(curContext);
            }

            if (_contextStack.Count != 0)
            {
                BaseContext lastContext = _contextStack.Peek();
                BaseView curView = Singleton<UIManager>.Instance.GetSingleUI(lastContext.ViewType).GetComponent<BaseView>();
                curView.OnResume(lastContext);

            }

        }
        //重写Pop(),Push()  实现OtherView到HighSocreView的切换
        public void Push(BaseContext nextContext,double s)
        {
            if (_contextStack.Count != 0)
            {
                BaseContext curContext = _contextStack.Peek();
                BaseView curView = Singleton<UIManager>.Instance.GetSingleUI(curContext.ViewType).GetComponent<BaseView>();
                curView.OnExit(curContext);
            }

            _contextStack.Push(nextContext);
            BaseView nextView = Singleton<UIManager>.Instance.GetSingleUI(nextContext.ViewType).GetComponent<BaseView>();
            nextView.OnEnter(nextContext);
        }
        //报错框
        //public void Push(BaseContext nextContext, string message)
        //{
        //    foreach(BaseContext baseContext in _contextStack)
        //    {
        //        if (!(baseContext is MainMenuContext) && !(baseContext is ModeContext))
        //        {

        //            BaseView curView = Singleton<UIManager>.Instance.GetSingleUI(baseContext.ViewType).GetComponent<BaseView>();
        //            if (baseContext is ShopContext)
        //            {
        //                curView.OnExit(baseContext);
        //            }
        //            else
        //            {
        //                curView.OnPause(baseContext);


        //            }
        //        }
 
        //    }
        //    _contextStack.Push(nextContext);
        //    BaseView nextView = Singleton<UIManager>.Instance.GetSingleUI(nextContext.ViewType).GetComponent<BaseView>();
        //    ((PopView)nextView).OnEnter(nextContext, message);
        //}

        public void Push(BaseContext nextContext, string message)
        {
            foreach (BaseContext baseContext in _contextStack)
            {
                int count = 0;
                if(baseContext is PopContext)
                {
                    count++;
                }
                if (count == 2)
                {
                    _contextStack.Pop();
                    return;
                }
                if (!(baseContext is MainMenuContext) && !(baseContext is ModeContext))
                {

                    BaseView curView = Singleton<UIManager>.Instance.GetSingleUI(baseContext.ViewType).GetComponent<BaseView>();
                    if (baseContext is ShopContext)
                    {
                        curView.OnExit(baseContext);
                    }
                    else
                    {
                        curView.OnPause(baseContext);


                    }
                }

            }
            _contextStack.Push(nextContext);
            BaseView nextView = Singleton<UIManager>.Instance.GetSingleUI(nextContext.ViewType).GetComponent<BaseView>();
            ((PopView)nextView).OnEnter(nextContext, message);
        }

        public void Pop(string s)
        {
            if (_contextStack.Count != 0)
            {
                BaseContext curContext = _contextStack.Peek();
                _contextStack.Pop();

                BaseView curView = Singleton<UIManager>.Instance.GetSingleUI(curContext.ViewType).GetComponent<BaseView>();
                curView.OnExit(curContext);
            }

            if (_contextStack.Count != 0)
            {
                BaseContext lastContext = _contextStack.Peek();
                BaseView curView = Singleton<UIManager>.Instance.GetSingleUI(lastContext.ViewType).GetComponent<BaseView>();
                curView.OnEnter(lastContext);
            }
        }

        //重写Pop(),Push()  实现商店界面的切换
        public void Push(BaseContext nextContext, int tag)
        {
            BaseContext curContext = _contextStack.Peek();
            if (curContext is ShopItemContext)
            {
                _contextStack.Pop();
                BaseView curView = Singleton<UIManager>.Instance.GetSingleUI(curContext.ViewType).GetComponent<BaseView>();
                curView.OnExit(curContext);
            }
            PlayerPrefs.SetInt("Tag", tag);
            _contextStack.Push(nextContext);
            BaseView nextView = Singleton<UIManager>.Instance.GetSingleUI(nextContext.ViewType).GetComponent<BaseView>();
            ((ShopItemView)nextView).OnEnter(nextContext,tag);
        }
        //重写Pop(),实现商店界面的退出
        public void Pop(int s)
        {
            //s==0退出ShopView,s == 1 ShopView从OnPause恢复
            if (s == 0)
            {
                while (_contextStack.Count > 1)
                {
                    BaseContext curContext = _contextStack.Peek();
                    _contextStack.Pop();

                    BaseView curView = Singleton<UIManager>.Instance.GetSingleUI(curContext.ViewType).GetComponent<BaseView>();
                    curView.OnExit(curContext);
                    if (curContext is ShopItemContext)
                    {
                        curView.DestroySelf();
                    }
                }

                BaseContext lastContext = _contextStack.Peek();
                BaseView cur = Singleton<UIManager>.Instance.GetSingleUI(lastContext.ViewType).GetComponent<BaseView>();
                cur.OnResume(lastContext);
            }
            else  if(s == 1){
                BaseContext curContext = _contextStack.Peek();
                _contextStack.Pop();

                BaseView curView = Singleton<UIManager>.Instance.GetSingleUI(curContext.ViewType).GetComponent<BaseView>();
                curView.OnExit(curContext);
                foreach (BaseContext baseContext in _contextStack)
                {
                    if (!(baseContext is MainMenuContext) )
                    {
                        curView = Singleton<UIManager>.Instance.GetSingleUI(baseContext.ViewType).GetComponent<BaseView>();
                        if (baseContext is ShopContext)
                        {
                            curView.OnEnter(baseContext);
                        }
                        else
                        {
                            curView.OnResume(baseContext);
                            curView.OnEnter(baseContext);
                        }
                    }

                }
            }

        }
        public BaseContext PeekOrNull()
        {
            if (_contextStack.Count != 0)
            {
                return _contextStack.Peek();
            }
            return null;
        }
    }
}
