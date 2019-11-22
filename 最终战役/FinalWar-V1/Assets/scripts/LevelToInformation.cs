using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace artest.AIThirdPerson
{
    public class LevelToInformation
    {
        //玩家信息-3
        //物理0 魔法1 cd2
        public static int[] getLevel(int l)
        {
            if (l == 0)
                return new int[] { 100, 100, 1 };
            else if (l == 1)
                return new int[] { 150, 150, 1 };
            else if (l == 2)
                return new int[] { 200, 200, 1 };
            else
                return new int[] { 200, 200, 1 };
        }
        //城堡信息-7
        //物理0 魔法1 护甲2 魔抗3 血量4 蓝量5 射速6
        public static int[] getCastle(int l)
        {
            if (l == 0)
                return new int[] { 100, 100, 50, 50, 4000, 500, 3 };
            else if (l == 1)
                return new int[] { 120, 120, 80, 80, 6000, 800, 3 };
            else if (l == 2)
                return new int[] { 150, 150, 100, 100, 8000, 1000, 3 };
            else
                return new int[] { 150, 150, 100, 100, 8000, 1000, 3 };
        }
        //防御塔-6
        //物理 魔法  射速
        public static int[] getDefence(int l)
        {
            if (l == 0)
                return new int[] { 100, 100, 3 };
            else if (l == 1)
                return new int[] { 120, 120, 2 };
            else if (l == 2)
                return new int[] { 150, 150, 1 };
            else
                return new int[] { 150, 150, 1 };
        }
        //技能-回血-3
        //回复0 times1 蓝耗2 cd3
        public static int[] getPropone(int l)
        {
            if (l == 0)
                return new int[] { 100, 10, 100, 25 };
            else if (l == 1)
                return new int[] { 200, 10, 150, 20 };
            else if (l == 2)
                return new int[] { 300, 10, 150, 15 };
            else
                return new int[] { 300, 10, 150,15 };
        }
        //技能-群体-3
        //魔法 蓝耗 cd
        public static int[] getProptwo(int l)
        {
            if (l == 0)
                return new int[] { 150, 100, 20 };
            else if (l == 1)
                return new int[] { 200, 100, 15 };
            else if (l == 2)
                return new int[] { 300, 100, 10 };
            else
                return new int[] { 300, 100, 10 };
        }
        //技能-单体-4
        //物理0 魔法1 蓝耗2 cd3
        public static int[] getPropthree(int l)
        {
            if (l == 0)
                return new int[] { 200, 200, 50, 15 };
            else if (l == 1)
                return new int[] { 250, 250, 50, 12 };
            else if (l == 2)
                return new int[] { 300, 300, 50, 10 };
            else
                return new int[] { 300, 300, 50, 10 };
        }
    }
}
