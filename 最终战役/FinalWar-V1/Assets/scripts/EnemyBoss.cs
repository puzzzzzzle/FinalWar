using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace artest.AIThirdPerson
{
    public class EnemyBoss : enemy
    {
        private void FixedUpdate()
        {
            //Debug.Log(status);
            //moveRound();

            if (status)
            {
                moveRound();
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
        //循环跑
        protected new void moveRound()
        {
            //Debug.Log(i.ToString());
            if (target[i] != null)
                agent.SetDestination(target[i].position);

            if (agent.remainingDistance > agent.stoppingDistance+2f)
                character.Move(agent.desiredVelocity, false, false);
            else
            {
                character.Move(Vector3.zero, false, false);
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
                    if (j > offset)
                    {
                        i = 0;
                        j = 0;
                    }
                    else
                    {
                        j++;
                    }
                }
            }
        }
    }
}
