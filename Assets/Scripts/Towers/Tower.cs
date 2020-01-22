using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Tower : MonoBehaviour
{
    public GameObject BoltPrefab;
    public int Damage = 1;
    public float AttackRate = 0.25f;
    public float AttackMovementSpeed = 10f;
    private float _attackCooldown = 0f;
    public float Range = 5f;

    public List<GameObject> EnemiesInRange;

    private GameObject _currentTarget = null;

    private void Start()
    {
        EnemiesInRange = new List<GameObject>();
    }

    private void Update()
    {
        if (_currentTarget == null && EnemiesInRange.Count > 0)
        {
            _currentTarget = EnemiesInRange[0];
        }

        _attackCooldown -= Time.deltaTime;
        Fire();
    }

    private void Fire()
    {
        if (!_currentTarget || !(_attackCooldown <= 0)) return;

        _attackCooldown = AttackRate;
        GameObject  bolt  = Instantiate(BoltPrefab, transform.position, Quaternion.identity);
        TowerAttack tbolt = bolt.GetComponent<TowerAttack>();
        tbolt.Target        = _currentTarget.transform.position;
        tbolt.MovementSpeed = AttackMovementSpeed;
        tbolt.Damage        = Damage;
    }

    private void OnValidate()
    {
        DrawCircle rangeIndicator = GetComponent<DrawCircle>();
        rangeIndicator.Xradius = Range;
        rangeIndicator.Yradius = Range;
        rangeIndicator.CreatePoints();

        CircleCollider2D collider2D = GetComponent<CircleCollider2D>();
        collider2D.radius = Range;
    }

    //TODO: DO a layer check and setup the layer matrix appropriately
    public void OnTriggerEnter2D(Collider2D col)
    {
        EnemiesInRange.Add(col.gameObject);
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject == _currentTarget)
        {
            _currentTarget = null;
        }

        EnemiesInRange.Remove(col.gameObject);
    }
}