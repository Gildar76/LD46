using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace GildarGaming.LD46
{
    public enum OrderType
    {
        pesticide,seeds,water,harvest, fertilizer, None
    }
    public class Order
    {
        bool isExecuting = false;
        GameObject target;
        Action callback;
        public OrderType orderType;
        Mover mover;
        public Order(GameObject target, OrderType orderType)
        {
            this.target = target;
            this.orderType = orderType;
        }

        public void Execute(Mover mover, Action executeCallback)
        {
            isExecuting = true;
            this.mover = mover;
            mover.StartMoving(target.transform.position);
            this.callback = executeCallback;
        }

        public void Update()
        {
            if (!isExecuting) return;
            if (mover.DestinationReach(2))
            {
                Debug.Log("Done executing order");
                isExecuting = false;
                callback.Invoke();

            }
        }
    }
}
