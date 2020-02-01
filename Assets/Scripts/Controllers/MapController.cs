using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public static MapController Instance;
    public List<Transform> Waypoints;
    public GameObject Road;
    public Dictionary<Vector2, bool> IsSpaceOccupied;

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
        Waypoints       = new List<Transform>();
        IsSpaceOccupied = new Dictionary<Vector2, bool>();
        foreach (Transform child in children)
        {
            if (child.gameObject == gameObject) continue;
            Waypoints.Add(child);
        }

        SetupOccupiedSpaces();
    }

    private void Update()
    {
        DrawDebugLines();
    }

    public static bool IsOccupied(Vector2 pos)
    {
        return Instance.IsSpaceOccupied.ContainsKey(pos);
    }

    private void SetupOccupiedSpaces()
    {
        for (int i = 0; i < Waypoints.Count - 1; i++)
        {
            Transform current = Waypoints[i];
            Transform next    = Waypoints[i + 1];
            float     dist    = Vector3.Distance(current.position, next.position);
            Vector3   unit    = next.position - current.position;
            unit.Normalize();
            for (int j = 0; j < dist; j++)
            {
                IsSpaceOccupied.Add(current.position + (unit * j), true);
                //Note: Uncomment to spawn in GameObjects per "tile" occupied. Occasionally useful for debugging.
                //Instantiate(Road, current.position + (unit * j), Quaternion.identity, this.gameObject.transform);
            }
        }
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