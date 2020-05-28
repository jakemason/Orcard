using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Building Card", menuName = "Cards/Building Card")]
public class BuildingCard : Card
{
    // @formatter:off 
    [Header("Building Effects")]
    [Space(20)]
    [Tooltip("These effects apply at the start of the each turn while the building is alive.")]
    public List<Effect> EachTurnEffects;
    [Space(20)]
    [Tooltip("These effects are applied to adjacent buildings.")]
    public List<Effect> AdjacencyEffects;
    
    public bool IsIndestructible; //Indestructible buildings cannot be removed by other cards.
    [Tooltip("This is the shadow sprite that will be cast beneath the building.")]
    public Sprite Shadow;
    
    // @formatter:on 

#if UNITY_EDITOR
    public override void OnValidate()
    {
        base.OnValidate();
        IsTargeted = true;

        string startEffects = " ";
        if (EachTurnEffects.Count > 0 && OverrideDefaultInstructionText == "")
        {
            startEffects = "Each Turn: ";
            foreach (Effect effect in EachTurnEffects)
            {
                if (effect == null) continue;
                startEffects += effect.InstructionText + " ";
            }
        }

        string adjacentEffects = "";
        if (AdjacencyEffects.Count > 0 && OverrideDefaultInstructionText == "")
        {
            foreach (Effect effect in AdjacencyEffects)
            {
                if (effect == null) continue;
                adjacentEffects += effect.InstructionText + " ";
            }
        }

        InstructionText += startEffects;
        InstructionText += adjacentEffects;
        if (IsIndestructible)
        {
            InstructionText += "Indestructible.";
        }
    }
#endif
}