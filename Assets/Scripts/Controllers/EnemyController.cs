using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    public Enemy Model;
    public Stack<Transform> WaypointsToFollow;

    private Transform _currentTarget;
    private Stack<Transform> _waypointsBackup;
    private float _speed = 1.0f;

    private void Start()
    {
        SpriteRenderer rend = GetComponent<SpriteRenderer>();
        rend.sprite = Model.Sprite;
        MarkAlive();
    }

    public void MarkAlive()
    {
        WaypointsToFollow = new Stack<Transform>(MapController.GetWaypoints());
        _waypointsBackup  = new Stack<Transform>(WaypointsToFollow);
        _speed            = Model.Speed;
        Health hp = GetComponent<Health>();
        hp.SetStartingHealth(Model.HP);
        transform.position = WaypointsToFollow.Peek().position;
        float   scale    = Random.Range(Model.MinSize, Model.MaxSize);
        Vector3 newScale = new Vector3(scale, scale, scale);
        transform.localScale = newScale;
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

        transform.position = Vector3.MoveTowards(transform.position, _currentTarget.position, _speed * Time.deltaTime);
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Core"))
        {
            Core core = col.gameObject.GetComponent<Core>();
            core.TakeDamage(Model.Damage);
            WaveController.Instance.EnemiesSpawned.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}