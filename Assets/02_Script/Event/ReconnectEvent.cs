using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ReconnectEvent : MonoBehaviour
{

    private List<RoadRoot> roads;

    private void Awake()
    {

        roads = FindObjectsOfType<RoadRoot>().ToList();

    }

    public void Event()
    {

        foreach(var road in roads)
        {
            road.ConnectRoad();

        }

    }

}
