using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

namespace GildarGaming.LD46
{
    public class Mover : MonoBehaviour
    {
        NavMeshAgent agent;
        Vector3 destination;
        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();

        }

        private void Start()
        {
           
        }

        public void StartMoving(Vector3 destination)
        {
            agent.SetDestination(destination);
            this.destination = destination;
        }

        public void StopMoving()
        {
            agent.isStopped = true;
        }

        public bool DestinationReach(float minRange = 0.1f)
        {
            if (Vector3.Distance(transform.position, destination) < minRange)
            {
                return true;
            }
            return false;
        }


    }
}
