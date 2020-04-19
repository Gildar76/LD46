using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GildarGaming.LD46
{
    public class InsectController : MonoBehaviour
    {
        float directiontimer = 0;
        float directionDelay = 10f;
        float moveMentspeed = 2f;
        float minX = -12f;
        float minZ = -12;
        float maxX = 12;
        float maxZ = 12;
        int food = 0;
        int foodToBreed = 3;
        Vector3 destination = new Vector3();
        Vector3 defaultPostion;
        private void Awake()
        {
            defaultPostion = new Vector3(12f, 0, 12f);
            destination = transform.position;
        }
        private void Update()
        {
            directiontimer += Time.deltaTime;
            if (directiontimer >= directionDelay)
            {
                directiontimer = 0;
                destination = new Vector3(Random.Range(minX, maxX), 1, Random.Range(minZ, maxZ));
                
            }
            transform.position = Vector3.MoveTowards(transform.position, destination, moveMentspeed * Time.deltaTime);
            

        }

        public void Feed()
        {
            food++;
            if (food >= foodToBreed)
            {
                GameObject.Instantiate(this, transform.position, Quaternion.identity);
                food = 0;
            }
        }
        public void Infect()
        {
            destination = defaultPostion;
            directiontimer = 0;
        }

    }
}
