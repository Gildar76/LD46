using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System;

namespace GildarGaming.LD46
{
    public class UIController : MonoBehaviour
    {
        public TMP_Text orderText;
        public Action<OrderType> updateOrder;
        public Action<int> scoreUpdate;
        public TMP_Text socreText;
        int score = 0;
        private void Awake()
        {
            scoreUpdate += OnUpdateScore;
            updateOrder += OnUpdateOrder;
        }

        private void OnUpdateOrder(OrderType orderTye)
        {
            orderText.text = orderTye.ToString();
        }

        private void OnUpdateScore(int addScore)
        {
            score += addScore;
            socreText.text = score.ToString();
        }
    }
}
