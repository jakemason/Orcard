using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    // @formatter:off
    public Enemy Model;
    public Stack<Transform> WaypointsToFollow;
    public EnemyHeadView HeadView;

    [Header("Orc Accessories")] 
    public SpriteRenderer Sword;
    public SpriteRenderer SmallHelm;
    public SpriteRenderer LargeHelm;
    public SpriteRenderer Shield;
    public SpriteRenderer Boots;
    public SpriteRenderer SmallShoulders;
    public SpriteRenderer LargeShoulders;
    public SpriteRenderer WarBanner;
    
    private Transform _currentTarget;
    private Stack<Transform> _waypointsBackup;
    private float _speed = 1.0f;
    // @formatter:on

    private void Start()
    {
        MarkAlive();
        SetupAccessoriesDisplay();
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

    //TODO: No idea if this is the most efficient way to do this. Probably not.
    private void SetupAccessoriesDisplay()
    {
        Sword.enabled          = Model.HasSword;
        SmallHelm.enabled      = Model.HasSmallHelm;
        LargeHelm.enabled      = Model.HasLargeHelm;
        Shield.enabled         = Model.HasShield;
        Boots.enabled          = Model.HasBoots;
        SmallShoulders.enabled = Model.HasSmallShoulders;
        LargeShoulders.enabled = Model.HasLargeShoulders;
        WarBanner.enabled      = Model.HasWarBanner;
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
            Core.TakeDamage(Model.Damage);
            WaveController.Instance.EnemiesRemainingInWave -= 1;
            WaveController.Instance.EnemiesSpawned.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}