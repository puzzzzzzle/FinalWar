using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace artest.AIThirdPerson
{
    public class GameLevel : MonoBehaviour
    {
        public GameObject Boss;
        public int maxEnemy = 10;
        public GameObject[] enemyPrefabs {
            get;set;
        }
        //代码改变，不需要更改
        public List<GameObject> enemys = new List<GameObject>();
        private void Start()
        {
            enemys = new List<GameObject>();
        }
        public Transform[] positions
        {
            get; set;
        }
        public int gameTime{
            get;set;
        }
        public int currTime
        {
            get; set;
        }
        public virtual void UpdateCalled(Transform main, Transform cam)
        {

        }

    }
}
