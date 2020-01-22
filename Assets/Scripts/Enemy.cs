using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Speed = 1.0f;
    public int HP;

    private Transform _currentTarget;
    public Stack<Transform> WaypointsToFollow;
    private Stack<Transform> _waypointsBackup;

    public void Start()
    {
        WaypointsToFollow  = new Stack<Transform>(Map.GetWaypoints());
        _waypointsBackup   = new Stack<Transform>(WaypointsToFollow);
        transform.position = WaypointsToFollow.Peek().position;
    }

    private void Update()
    {
        if (_currentTarget == null || Vector3.Distance(transform.position, _currentTarget.position) < 0.1f)
        {
            if (WaypointsToFollow.Count == 0)
            {
                WaypointsToFollow = new Stack<Transform>(_waypointsBackup);
            }

            _currentTarget = WaypointsToFollow.Pop();
        }

        transform.position = Vector3.MoveTowards(transform.position, _currentTarget.position, Speed * Time.deltaTime);
    }
}