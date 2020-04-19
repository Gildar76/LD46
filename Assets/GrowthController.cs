using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace GildarGaming.LD46
{
    public class GrowthController : MonoBehaviour
    {
        public Canvas waterIcon;
        public float deathDelay = 30f;
        public float growthdelay = 10f;
        public float waterSupply = 0f;
        public bool seeded = false;
        public float pesticideSupply = 0f;
        public float fertilizerSupply = 0;
        MeshRenderer renderer;
        public bool isGrown = false;
        Material plantMat;
        private float growthTimer = 0;
        private float deathTimer;
        public GameObject seedPod;
        Color defaultColor;
        public Color dyingColor;
        UIController uiCnt;
        AudioSource audio;
        private void Awake()
        {    
            waterIcon = GetComponentInChildren<Canvas>();
            renderer = GetComponentInChildren< MeshRenderer>(true);
            plantMat = GetComponentInChildren<MeshRenderer>(true).material;
            defaultColor = plantMat.color;
            waterIcon.enabled = false;
        }

        private void Start()
        {
             uiCnt = FindObjectOfType<UIController>();
            audio = GetComponent<AudioSource>();

        }
        private void Update()
        {
            if (deathTimer > deathDelay)
            {
                KillPlant();
            }
            if (!seeded) return;
            
            float timeToAdd = Time.deltaTime;
            
            if (fertilizerSupply > 0)
            {
                timeToAdd *= 2.5f;
                fertilizerSupply -= 0.1f * Time.deltaTime;

            }
            if (waterSupply > 0)
            {
                waterIcon.enabled = false;
                waterIcon.gameObject.SetActive(false);
                deathTimer = 0;
                timeToAdd *= 1.5f;
                waterSupply -= 0.01f * Time.deltaTime;
            } else
            {
                if (isGrown)
                {
                    
                    waterIcon.enabled = true;
                    waterIcon.gameObject.SetActive(true);
                    deathTimer += Time.deltaTime;
                    if (deathTimer >= deathDelay / 2 && plantMat.color == defaultColor)
                    {
                        plantMat.color = dyingColor;
                    }
                }

            }
            growthTimer += timeToAdd;



            if (growthTimer > growthdelay && !isGrown)
            {
                uiCnt.scoreUpdate(50);
                isGrown = true;
                audio.Play();
                renderer.enabled = true;
            } 


        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("Triggering" + other.tag);
            if (other.gameObject.tag == "Player")
            {

                RefillPlantSupplies(other.gameObject);
            } else if(other.tag == "Insect")
            {
                if (!isGrown) return;
                if (pesticideSupply <= 0.1)
                {
                    other.gameObject.GetComponent<InsectController>().Feed();
                    KillPlant();
                } else
                {
                    pesticideSupply -= 0.1f;
                    uiCnt.scoreUpdate(5);
                    other.gameObject.GetComponent<InsectController>().Infect();
                }

            }
        }

        private void RefillPlantSupplies(GameObject playerObject)
        {
            Debug.Log("Doing plant stuff");
            TractorController ctrl = playerObject.GetComponent<TractorController>();
            switch (ctrl.currentOrderType)
            {
                case OrderType.seeds:
                    uiCnt.scoreUpdate(1);
                    seeded = true;
                    seedPod.SetActive(true);
                    break;
                case OrderType.fertilizer:
                    uiCnt.scoreUpdate(2);
                    fertilizerSupply = 1f;
                    break;
                case OrderType.pesticide:
                    uiCnt.scoreUpdate(2);
                    pesticideSupply = 1f;
                    break;
                case OrderType.water:
                    uiCnt.scoreUpdate(1);
                    plantMat.color = defaultColor;
                    waterSupply = 1;
                    break;


            }
        }

        private void KillPlant()
        {
            if (isGrown) uiCnt.scoreUpdate(-25);
            seeded = false;
            deathTimer = 0;
            plantMat.color = defaultColor;
            growthTimer = 0;
            waterSupply = 0;
            fertilizerSupply = 0;
            renderer.enabled = false;
            isGrown = false;
            seedPod.SetActive(false);
            waterIcon.enabled = false;
            waterIcon.gameObject.SetActive(false);

        }
    }
}
