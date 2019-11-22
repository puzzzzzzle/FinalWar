using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace artest.AIThirdPerson
{
    public class EnemyArchar : enemy
    {
        private void Update()
        {
            //Debug.Log("run");
            if (blood <= 0)
            {
                if (this.gameObject != null)
                {
                    enemyDestory();
                }

            }
            else
            {
                this.gameObject.transform.LookAt(GameObject.Find("main").transform);
            }
          
        }
        private void FixedUpdate()
        {
            //Debug.Log(status);
            //moveRound();
            if (status)
            {
                moveOnce();
            }
            else
            {
                character.Move(gameObject.transform.position, false, false);
                //Debug.Log(currPauseTime);
                currPauseTime++;
                if (currPauseTime >= pauseTime)
                {
                    //Debug.Log("true0");
                    currPauseTime = 0;
                    status = true;
                    if (a != null)
                    {
                        a.PlayQueued(walk);
                    }
                    gameObject.transform.LookAt(GameObject.Find("main").transform);
                }

            }
        }
        //只跑一次
        protected new void moveOnce()
        {
            //Debug.Log(i.ToString());
            if (target[i] != null)
                agent.SetDestination(target[i].position);

            if (agent.remainingDistance > agent.stoppingDistance+3)
            {
                character.Move(agent.desiredVelocity, false, false);
            }
            else
            {
                character.Move(Vector3.zero, false, false);

                Animation a = gameObject.GetComponent<Animation>();
                if (a != null)
                {
                    a.PlayQueued(walk);
                    a[walk].wrapMode = WrapMode.Once;
                }
                if (i < target.Length - 1)
                {
                    if (j > offset)
                    {
                        i++;
                        j = 0;
                    }
                    else
                    {
                        j++;
                    }
                }
                else
                {
                    //Animation a = gameObject.GetComponent<Animation>();
                    if (a != null)
                    {
                        a.CrossFade(stand);
                        a[stand].wrapMode = WrapMode.Loop;
                    }
                }
            }
        }
    }
}