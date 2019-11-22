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
    public class EndRoot : MonoBehaviour
    {

        public void Start()
        {
            Singleton<UIManager>.Create();
            Singleton<EndContextManager>.Create();
        }

    }
}
