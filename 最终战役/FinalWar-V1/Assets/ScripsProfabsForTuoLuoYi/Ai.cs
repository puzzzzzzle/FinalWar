using System;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
    [RequireComponent(typeof(ThirdPersonCharacter))]
    public class Ai : MonoBehaviour
    {
        public UnityEngine.AI.NavMeshAgent agent { get; private set; }             // the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling
        public Transform target;                                    // target to aim for
        public int offset = 10;
        public GameObject dragon;

        private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<UnityEngine.AI.NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();

            agent.updateRotation = false;
            agent.updatePosition = true;
        }


        private void Update()
        {
            if (target != null)
            {
                agent.SetDestination(target.position);
                //Debug.Log(agent.remainingDistance + ":" + (agent.stoppingDistance + offset)+":"+ agent.stoppingDistance);
            }

            if (agent.remainingDistance > (agent.stoppingDistance + offset))
            {
                character.Move(agent.desiredVelocity, false, false);
                //Debug.Log("move");
            }
            else
                character.Move(Vector3.zero, false, false);
            dragon.transform.position = new Vector3(this.gameObject.transform.position.x, 10, this.gameObject.transform.position.z);
        }


        public void SetTarget(Transform target)
        {
            this.target = target;
        }
    }
}
