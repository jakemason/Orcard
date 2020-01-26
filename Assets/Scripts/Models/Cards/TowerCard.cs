using UnityEngine;

[CreateAssetMenu(fileName = "New Tower Card", menuName = "Cards/Tower Card")]
public class TowerCard : Card
{
    // @formatter:off 
    [Header("Tower Attributes")] 
    [Space(20)]
    public GameObject BoltPrefab;
    public int Damage = 1;
    public float AttackRate = 0.25f;
    public float AttackMovementSpeed = 10f;
    public float Range = 5f;
    // @formatter:on
}