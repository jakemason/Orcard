using UnityEngine;

[CreateAssetMenu(fileName = "New Tower Card", menuName = "Cards/Tower Card")]
public class TowerCard : Card
{
    // @formatter:off 
    [Header("Tower Attributes")] 
    [Space(20)]
    public GameObject BoltPrefab;
    public int Damage = 1;
    public float AttacksPerSecond = 1;
    public float AttackMovementSpeed = 10f;
    public float Range = 5f;
    
    [Header("Calculated Tower Stats")] 
    [ReadOnly] public float DamagePerSecond;
    [ReadOnly] public float AttackRate = 0.25f;
    // @formatter:on

    public override void OnValidate()
    {
        base.OnValidate();
        AttackRate      = 1                / (float) AttacksPerSecond;
        DamagePerSecond = (1 / AttackRate) * Damage;
    }
}