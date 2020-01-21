using System;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public static Map Instance;
    public List<Transform> Waypoints;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public static List<Transform> GetWaypoints()
    {
        return Instance.Waypoints;
    }
}