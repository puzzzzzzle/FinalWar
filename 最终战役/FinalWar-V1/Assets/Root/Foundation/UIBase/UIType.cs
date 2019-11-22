using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 *	
 *  Define View's Path And Name
 *
 *	by Xuanyi
 *
 */

namespace MoleMole
{
	public class UIType {

        public string Path { get; private set; }

        public string Name { get; private set; }

        public UIType(string path)
        {
            Path = path;
            Name = path.Substring(path.LastIndexOf('/') + 1);
        }

        public override string ToString()
        {
            return string.Format("path : {0} name : {1}", Path, Name);
        }

        public static readonly UIType MainMenu = new UIType("View/MainMenuView");     
        public static readonly UIType HighScore = new UIType("View/HighScoreView");
        public static readonly UIType Land = new UIType("View/LandView");
        public static readonly UIType Reigister = new UIType("View/ReigisterView");
        public static readonly UIType Pop = new UIType("View/PopView");
        public static readonly UIType Other = new UIType("View/OtherTest");
        public static readonly UIType Help = new UIType("View/HelpView");
        public static readonly UIType Victory = new UIType("View/VictoryView");
        public static readonly UIType Defeat = new UIType("View/DefeatView");
        public static readonly UIType Shop = new UIType("View/ShopView");
        public static readonly UIType ShopItem = new UIType("View/ShopItemView");
        public static readonly UIType Select = new UIType("View/SelectView");
        public static readonly UIType Mode = new UIType("View/SelectMode");
    }
}
