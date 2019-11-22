using artest.AIThirdPerson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace artest.AIThirdPerson
{
    
    public class GameLevel1 : GameLevel
    {

        int offset = 100;
        int currOffset = 0;
        int i = 0;
        //Transform main;
        //Transform cam;
        public override void UpdateCalled( Transform main,Transform cam)
        {
            if (currTime > currOffset&&enemys.Count<maxEnemy)
            {
                currOffset += offset;
                //GameObject enemy = Instantiate(enemyPrefabs[0], positions[i].localPosition, Quaternion.identity, GameObject.Find("ImageTarget-Image-cam1").transform);
                //enemy.GetComponent<gun>().mainTarget = GameObject.Find("main").transform;
                GameObject enemy = Instantiate(enemyPrefabs[0], positions[i].position, Quaternion.identity,cam);		
				enemy.GetComponent<gun>().mainTarget = main;
                Transform[] temp = new Transform[1];
                temp[0] = positions[i];
                enemy.GetComponent<enemy>().target =temp;
                //Debug.Log("生产一个敌人");
                enemys.Add(enemy);
                //Debug.Log(enemys.Count);
                if (i < 3)
                {
                    i++;
                }
                else
                {
                    i = 0;
                }
            }
        }
    }
}
