using System;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

namespace artest.AIThirdPerson
{
    [RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
    [RequireComponent(typeof(ThirdPersonCharacter))]
    public class enemy : MonoBehaviour
    {
        public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling
        public Transform[] target;                                    // target to aim for
        public int offset = 10;
        public float blood = 100;
        public float maxBlood = 100;
        public float huajia = 1;
        public float mokang = 1;
		public int defeng =0;
		public int jinBi =0;
        public bool status = true;
        public int pauseTime = 5;
        protected int currPauseTime = 0;
        public string walk = "Walk2";
        public string stand = "stand";
        public string dying = "Skeleton_archer_dying";
        public string damaged = "Skeleton_hit_from_front";
        protected bool statResult = true;
        //public string run = "run";
        protected Animation a ;
        bool walkstat = true;

        protected GameController enemys;
        private void Start()
        {
            a = gameObject.GetComponent<Animation>();
            enemys = GameObject.Find("main").GetComponent<GameController>();
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();

            agent.updateRotation = false;
            agent.updatePosition = true;

        }

        protected int i = 0;
        protected int j = 0;
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
        //循环跑
        protected void moveRound()
        {
            //Debug.Log(i.ToString());
            if (target[i] != null)
                agent.SetDestination(target[i].position);

            if (agent.remainingDistance > agent.stoppingDistance)
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
        //只跑一次
        protected void moveOnce()
        {
            //Debug.Log(i.ToString());
            if (target[i] != null)
                agent.SetDestination(target[i].position);

            if (agent.remainingDistance > agent.stoppingDistance)
            {
                character.Move(agent.desiredVelocity, false, false);
                Animation a = gameObject.GetComponent<Animation>();
                if (a != null)
                {
                    if (walkstat)
                    {
                        a.CrossFade(walk);
                        walkstat = false;
                    }
                    else
                    {
                        a.PlayQueued(walk);
                        a[walk].wrapMode = WrapMode.Once;
                    }
                }
            }
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
                    //Animation a = gameObject.GetComponent<Animation>();
                    if (a != null)
                    {
                        a.CrossFade(stand);
                        a[stand].wrapMode = WrapMode.Loop;
                    }
                }
            }
        }
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
        }

        public void SetTarget(Transform[] target)
        {
            this.target = target;
        }

        public void damageByMofa(float dam)
        {
            //blood -= 1 / mokang * dam;
            blood -= (float)Math.Ceiling(100 / (100 + mokang) * dam);
            if (a != null&& blood>0)
            {
                a.CrossFade(damaged);
                a[damaged].wrapMode = WrapMode.Once;
            }

        }
        public void damageByWuli(float dam)
        {
            //blood -= 1 / huajia * dam;
            blood -= (float)Math.Ceiling(100 / (100 + huajia) * dam);
            Animation a = gameObject.GetComponent<Animation>();
            if (a != null && blood > 0)
            {
                a.CrossFade(damaged);
                a[damaged].wrapMode = WrapMode.Once;
            }
        }
        public void enemyDestory()
        {
            if (statResult)
            {
                enemys.addPOint(defeng, jinBi);
                statResult = false;
            }
            //Debug.Log("<=0");
            enemys.gameLevel.enemys.Remove(this.gameObject);
            Destroy(this.gameObject, 2);
            Animation a = gameObject.GetComponent<Animation>();
            if (a != null)
            {
                a.CrossFade(dying);
                a[dying].wrapMode = WrapMode.Once;
            }
            
        }
        public void pause()
        {
            status = false;
        }
    }
}