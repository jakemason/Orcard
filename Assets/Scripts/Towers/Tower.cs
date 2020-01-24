using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class Tower : MonoBehaviour
{
    public TowerModel Model;
    public List<GameObject> EnemiesInRange;

    private float _attackCooldown = 0f;
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

        _attackCooldown = Model.AttackRate;
        GameObject  bolt  = Instantiate(Model.BoltPrefab, transform.position, Quaternion.identity);
        TowerAttack tbolt = bolt.GetComponent<TowerAttack>();
        tbolt.Target        = _currentTarget.transform.position;
        tbolt.MovementSpeed = Model.AttackMovementSpeed;
        tbolt.Damage        = Model.Damage;
    }

    private void OnValidate()
    {
        DrawCircle rangeIndicator = GetComponent<DrawCircle>();
        if (rangeIndicator)
        {
            rangeIndicator.Xradius = Model.Range;
            rangeIndicator.Yradius = Model.Range;
            rangeIndicator.CreatePoints();
        }


        CircleCollider2D circleCollider2D = GetComponent<CircleCollider2D>();
        if (circleCollider2D)
        {
            circleCollider2D.radius = Model.Range;
        }
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