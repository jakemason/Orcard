using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tower Card", menuName = "Cards/Tower Card")]
public class TowerCard : Card
{
    // @formatter:off 
    [Header("Building Effects")] 
    [Space(20)]
    [Tooltip("These effects apply at the start of the each turn while the tower is alive.")]
    public List<Effect> EachTurnEffects;
    
    [Header("Tower Attributes")] 
    [Space(20)]
    public GameObject BoltPrefab;
    public int Damage = 0;
    public float AttacksPerSecond = 0;
    public float AttackMovementSpeed = 0f;
    public float Range = 0f;
    
    //TODO: This is just a temporary way to quickly differentiate different towers
    public Color Tint = Color.white;
    
    [Header("Calculated Tower Stats")] 
    [ReadOnly] public float DamagePerSecond;
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
        base.OnValidate();
        UpdateComputedValues();
        IsTargeted = true;

        string startEffects = " ";
        if (EachTurnEffects.Count > 0 && OverrideDefaultInstructionText == "")
        {
            startEffects = "Each turn ";
            foreach (Effect effect in EachTurnEffects)
            {
                if (effect == null) continue;
                startEffects += effect.InstructionText + " ";
            }
        }

        InstructionText += startEffects;
    }
#endif
}