using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace GildarGaming.LD46
{
    public class OrderButton : MonoBehaviour
    {
        OrderButton[] allButtons;
        public OrderType orderType;
        public GameObject target;
        TractorController tract;
        Order order;
        Button button;
        private void Awake()
        {
            order = new Order(target, orderType);
        }

        private void Start()
        {
            button = GetComponent<Button>();
            allButtons = FindObjectsOfType<OrderButton>();
            tract = FindObjectOfType<TractorController>();
        }
        public void OnCLick()
        {
            tract.ExecuteOrder(order, this);
            foreach (var btn in allButtons)
            {
                btn.DisableButton();
            }

        }

        void EnableButton()
        {
            button.interactable = true;
        }

        void DisableButton()
        {
            button.interactable = false;
        }
        public void DoneExecuting()
        {
            foreach (var btn in allButtons)
            {
                btn.EnableButton();
            }
        }
    }
}
