using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace MoleMole
{
    public class ShopContext : BaseContext
    {
        public ShopContext() : base(UIType.Shop)
        {

        }
    }

    public class ShopView : AnimateView
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
        //人物升级按钮
        public void CharacterUp()
        {
            int shop = PlayerPrefs.GetInt("Shop");
            if (shop == 0)
            {
                Singleton<ContextManager>.Instance.Push(new ShopItemContext(), 0);
            }
            else if (shop == 1)
            {
                Singleton<EndContextManager>.Instance.Push(new ShopItemContext(), 0);
            }
        }
        //城堡升级按钮
        public void CastleUp()
        {
            int shop = PlayerPrefs.GetInt("Shop");
            if (shop == 0)
            {
                Singleton<ContextManager>.Instance.Push(new ShopItemContext(), 1);
            }
            else if (shop == 1)
            {
                Singleton<EndContextManager>.Instance.Push(new ShopItemContext(), 1);
            }

        }
        //城堡升级按钮
        public void DefenceUp()
        {
            int shop = PlayerPrefs.GetInt("Shop");
            if (shop == 0)
            {
                Singleton<ContextManager>.Instance.Push(new ShopItemContext(), 2);
            }
            else if (shop == 1)
            {
                Singleton<EndContextManager>.Instance.Push(new ShopItemContext(), 2);
            }

        }
        //道具一购买按钮
        public void BuyOne()
        {
            int shop = PlayerPrefs.GetInt("Shop");
            if (shop == 0)
            {
                Singleton<ContextManager>.Instance.Push(new ShopItemContext(), 3);
            }
            else if (shop == 1)
            {
                Singleton<EndContextManager>.Instance.Push(new ShopItemContext(), 3);
            }

        }
        //道具二购买按钮
        public void BuyTwo()
        {
            int shop = PlayerPrefs.GetInt("Shop");
            if (shop == 0)
            {
                Singleton<ContextManager>.Instance.Push(new ShopItemContext(), 4);
            }
            else if (shop == 1)
            {
                Singleton<EndContextManager>.Instance.Push(new ShopItemContext(), 4);
            }

        }
        //道具三购买按钮
        public void BuyThree()
        {
            int shop = PlayerPrefs.GetInt("Shop");
            if (shop == 0)
            {
                Singleton<ContextManager>.Instance.Push(new ShopItemContext(), 5);
            }
            else if (shop == 1)
            {
                Singleton<EndContextManager>.Instance.Push(new ShopItemContext(), 5);
            }

        }

        public void Back()
        {
            int shop = PlayerPrefs.GetInt("Shop");
            if(shop == 0)
            {
                Singleton<ContextManager>.Instance.Pop(0);
            }else if (shop == 1)
            {
                Singleton<EndContextManager>.Instance.Pop(0);
            }
        }

    }
}