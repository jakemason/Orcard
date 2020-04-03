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
    
    [Tooltip("This is just a temporary way to quickly differentiate different towers while we don't have art assets.")]
    public Color Tint = Color.white;
    
#if UNITY_EDITOR
    public override void OnValidate()
    {
        base.OnValidate();
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