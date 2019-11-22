using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 *	
 *  
 *
 *	by Xuanyi
 *
 */

namespace MoleMole
{
    public class LandRoot : MonoBehaviour
    {

        public void Start()
        {
            Singleton<UIManager>.Create();
            Singleton<LandContextManager>.Create();
        }

    }
}
