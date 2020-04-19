using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace GildarGaming.LD46
{
    
    public class GameManager : MonoBehaviour
    {
        public GrowthController[] plants;
        public float plantTimer = 0;
        public float miniMulGameOverDelay = 180;
           
        private void Start()
        {
            plants = FindObjectsOfType<GrowthController>();
        }

        private void Update()
        {
            plantTimer += Time.deltaTime;
            if (plantTimer >= miniMulGameOverDelay)
            {

                CheckLivePlatns();
            }
        }
        void CheckLivePlatns()
        {
            foreach (var plant in plants)
            {
                if (plant.isGrown) return;
            }
            GameOver();

        }
        
        void GameOver()
        {
            SceneManager.LoadScene(2);

        }

    }
}
