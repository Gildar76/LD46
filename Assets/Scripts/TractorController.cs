using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using System;

namespace GildarGaming.LD46
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class TractorController : MonoBehaviour
    {
        public ParticleSystem seedingParticles;
        public ParticleSystem pesticideParticles;
        public ParticleSystem waterParticles;
        public ParticleSystem fertilizerParticles;

        Queue<Order> orderQueue;
        NavMeshAgent agent;
        Waypoint[] waypoints;
        Waypoint currentWaypoint;
        Mover mover;
        int wayPointIndex = 0;
        bool isMoving;
        float seeds = 0;
        float pesticide = 0;
        float water = 0;
        Action orderCallback;
        OrderButton currentOrderButton;
        Order currentOrder;
        public OrderType currentOrderType;
        UIController cnt;
        private void Awake()
        {
            currentOrderType = OrderType.None;
            orderCallback += OrderCallBack;
            orderQueue = new Queue<Order>();
            agent = GetComponent<NavMeshAgent>();
            mover = GetComponent<Mover>();

        }

        private void OrderCallBack()
        {
            Debug.Log("Invoked orderCallback");
            switch(currentOrder.orderType)
            {
                case OrderType.seeds:
                    seeds = 1;
                    break;
                case OrderType.pesticide:
                    pesticide = 1;
                    break;
                case OrderType.water:
                    water = 1;
                    break;

            }
            currentOrderType = currentOrder.orderType;
            currentOrder = null;
            currentOrderButton.DoneExecuting();
            currentOrderButton = null;
        }

        private void Start()
        {
            waypoints = FindObjectsOfType<Waypoint>();
            SetNextWaypoint();
            isMoving = true;
            mover.StartMoving(currentWaypoint.transform.position);
        }

        internal void ExecuteOrder(Order order, OrderButton orderButton)
        {
            fertilizerParticles.Stop();
            pesticideParticles.Stop();
            waterParticles.Stop();
            seedingParticles.Stop();

            wayPointIndex = -1;
            this.currentOrderButton = orderButton;
            this.currentOrder = order;
            this.currentOrder.Execute(mover, orderCallback);
            isMoving = false;
            if (cnt == null)
            {
                cnt = FindObjectOfType<UIController>();
                
            }
            cnt.updateOrder(currentOrder.orderType);
        }

        private void Update()
        {
            if (currentOrder != null)
            {
                currentOrder.Update();
                return;
            }
            if (isMoving)
            {
                if (mover.DestinationReach())
                {
                    SetNextWaypoint();
                    mover.StartMoving(currentWaypoint.transform.position);
                }

            } else
            {
                SetNextWaypoint();
                mover.StartMoving(currentWaypoint.transform.position);
                isMoving = true;
            }
            if (currentWaypoint.index > 0 && isMoving)
            {
                switch(currentOrderType)
                {
                    case OrderType.seeds:
                        Seed();
                        break;
                    case OrderType.fertilizer:
                        fertilizer();
                        break;
                    case OrderType.pesticide:
                        Pesticide();
                        break;
                    case OrderType.water:
                        WaterPlants();
                        break;
                }
            }
        }

        private void fertilizer()
        {
            if (!fertilizerParticles.isPlaying)
            {
                fertilizerParticles.Play();
            }
        }

        private void WaterPlants()
        {
            if (!waterParticles.isPlaying)
            {
                waterParticles.Play();
            }
        }

        private void Pesticide()
        {
            if (!pesticideParticles.isPlaying)
            {
                pesticideParticles.Play();
            }
        }

        private void Seed()
        {
            if (!seedingParticles.isPlaying)
            {
                seedingParticles.Play();
            }
        }

        void SetNextWaypoint()
        {
            wayPointIndex++;
            
            currentWaypoint = Waypoint.GetWayPointByIndex(wayPointIndex);
            Debug.Log(currentWaypoint.index);
            

        }

    }
}
