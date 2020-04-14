using UnityEngine;

[CreateAssetMenu(fileName = "New Tower Card", menuName = "Cards/Tower Card")]
public class TowerCard : BuildingCard
{
    // @formatter:off
    [Header("Tower Attributes")] 
    [Space(20)]
    public GameObject BoltPrefab;
    [ReadOnly] public Effect ConstructionEffect;

    public int Damage = 0;
    public float AttacksPerSecond = 0;
    public float AttackMovementSpeed = 0f;
    public float Range = 0f;
    public int MaxAmmo;

    [Header("Calculated Tower Stats")] [ReadOnly]
    public float DamagePerSecond;

    [ReadOnly] public float AttackRate = 0.25f;
    // @formatter:on

    public void UpdateComputedValues()
    {
        AttackRate      = 1                / AttacksPerSecond;
        DamagePerSecond = (1 / AttackRate) * Damage;
    }

#if UNITY_EDITOR
    public override void OnValidate()
    {
        UpdateComputedValues();
        if (!ImmediateEffects.Contains(ConstructionEffect))
        {
            ImmediateEffects.Add(ConstructionEffect);
        }

        base.OnValidate();
    }
#endif
}