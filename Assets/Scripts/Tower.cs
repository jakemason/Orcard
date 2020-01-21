using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Tower : MonoBehaviour
{
    public int Damage = 1;
    public float AttackRate = 0.25f;
    private float _attackCooldown = 0f;
    public float Range = 5f;

    public List<GameObject> EnemiesInRange;

    private void Start()
    {
        EnemiesInRange = new List<GameObject>();
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
        EnemiesInRange.Remove(col.gameObject);
    }
}