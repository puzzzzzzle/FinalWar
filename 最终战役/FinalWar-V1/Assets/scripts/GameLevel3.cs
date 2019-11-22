﻿using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;
namespace artest.AIThirdPerson
{
    public class GameLevel3 : GameLevel
    {

        int offset = 100;
        int currOffset = 0;
        int i = 0;
        bool isBoss = true;
        
        //Transform main;
        //Transform cam;
        public override void UpdateCalled(Transform main, Transform cam)
        {
            if (isBoss)
            {

                Boss = Instantiate(enemyPrefabs[4], positions[i].position, Quaternion.identity, cam);
                Boss.GetComponent<gun>().bullet.wuliDamage = 100;
                Boss.GetComponent<gun>().bullet.mofaDamage = 100;
                Boss.GetComponent<gun>().mainTarget = main;

                Boss.GetComponent<enemy>().target = positions;
                //Debug.Log("Boss!!!!!!!! level3");
                enemys.Add(Boss);
                isBoss = false;
            }
            if (currTime > currOffset && enemys.Count < maxEnemy)
            {
                if (currTime > currOffset && enemys.Count < maxEnemy)
                {
                    System.Random ran = new System.Random();
                    int RandKey = ran.Next(0, 100);

                    currOffset += offset;
                    GameObject enemy;
                    Transform[] temp;
                    //GameObject enemy = Instantiate(enemyPrefabs[0], positions[i].localPosition, Quaternion.identity, GameObject.Find("ImageTarget-Image-cam1").transform);
                    //enemy.GetComponent<gun>().mainTarget = GameObject.Find("main").transform;
                    if (RandKey > 80)
                    {
                        enemy = Instantiate(enemyPrefabs[0], positions[i].position, Quaternion.identity, cam);
                        enemy.GetComponent<gun>().bullet.wuliDamage = 15;
                        enemy.GetComponent<gun>().bullet.mofaDamage = 0;
                        temp = new Transform[]
                        {
                        positions[i]
                        };
                        //Debug.Log("Enemy1 level2");
                    }
                    else if (RandKey > 30)
                    {
                        enemy = Instantiate(enemyPrefabs[1], positions[i].position, Quaternion.identity, cam);
                        enemy.GetComponent<gun>().bullet.wuliDamage = 15;
                        enemy.GetComponent<gun>().bullet.mofaDamage = 15;
                        temp = new Transform[]{
                        positions[i]};
                        //Debug.Log("Enemy2 level2");
                    }
                    else if (RandKey > 10)
                    {
                        enemy = Instantiate(enemyPrefabs[2], positions[i].position, Quaternion.identity, cam);
                        enemy.GetComponent<gun>().bullet.wuliDamage = 40;
                        enemy.GetComponent<gun>().bullet.mofaDamage = 0;
                        temp = new Transform[] { GameObject.Find("main").transform };
                        //Debug.Log("Enemy3 level2");
                    }
                    else
                    {
                        enemy = Instantiate(enemyPrefabs[3], positions[i].position, Quaternion.identity, cam);
                        enemy.GetComponent<gun>().bullet.wuliDamage = 100;
                        enemy.GetComponent<gun>().bullet.mofaDamage = 0;
                        temp = new Transform[] { GameObject.Find("main").transform };
                        //Debug.Log("Enemy4 level2");
                    }

                    enemy.GetComponent<gun>().mainTarget = main;

                    enemy.GetComponent<enemy>().target = temp;
                    //Debug.Log("生产一个敌人");
                    enemys.Add(enemy);
                    //Boss.gameObject.GetComponent<enemy>().pause();
                    //Boss.gameObject.transform.LookAt(GameObject.Find("main").transform);
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
}