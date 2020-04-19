using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GildarGaming.LD46
{
    
    public class Waypoint : MonoBehaviour
    {
        public static Waypoint[] wayPoints;
        public int index = 0;

        public static Waypoint GetWayPointByIndex(int wayPointIndex)
        {
            
            if (wayPoints == null)
            {
                wayPoints = FindObjectsOfType<Waypoint>();
            }
            wayPointIndex = wayPointIndex % wayPoints.Length;
            foreach (var wp in wayPoints)
            {
                if (wp.index == wayPointIndex)
                {
                    return wp;
                }
            }
            return null;
        }
    }
}
