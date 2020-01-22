using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Map : MonoBehaviour
{
    public static Map Instance;
    public List<Transform> Waypoints;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        Transform[] children = GetComponentsInChildren<Transform>();
        Waypoints = new List<Transform>();
        foreach (Transform child in children)
        {
            if (child.gameObject == gameObject) continue;
            Waypoints.Add(child);
        }
    }

    private void Update()
    {
        DrawDebugLines();
    }

    private void OnValidate()
    {
        DrawDebugLines();
    }

    private void DrawDebugLines()
    {
        for (int i = 0; i < Waypoints.Count - 1; i++)
        {
            Vector3 start = Waypoints[i].transform.position;
            Vector3 end   = Waypoints[i + 1].transform.position;
            Debug.DrawLine(start, end, Color.green);
        }
    }

    public static IEnumerable<Transform> GetWaypoints()
    {
        return Instance.Waypoints;
    }
}