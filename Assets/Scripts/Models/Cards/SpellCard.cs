using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Spell Card", menuName = "Cards/Spell Card")]
public class SpellCard : Card
{
    // @formatter:off 
    [Header("Turn-based Effects:")]
    [Space(20)]
    [Tooltip("The effects this card generates at the end of every turn.")]
    public List<Effect> PermanentStartOfTurnEffects;

    [Tooltip("These effects apply at the start of the next turn, and then are disabled.")]
    public List<Effect> StartOfNextTurnEffects;
    // @formatter:on

    public override void OnValidate()
    {
        base.OnValidate();
        string permanentEffects = "";
        if (PermanentStartOfTurnEffects.Count > 0 && OverrideDefaultInstructionText == "")
        {
            permanentEffects = "Each turn ";
            foreach (Effect effect in PermanentStartOfTurnEffects)
            {
                if (effect == null) continue;
                permanentEffects += effect.InstructionText + " ";
            }
        }

        string tempEffects = "";
        if (StartOfNextTurnEffects.Count > 0 && OverrideDefaultInstructionText == "")
        {
            tempEffects = "Each turn ";
            foreach (Effect effect in StartOfNextTurnEffects)
            {
                if (effect == null) continue;
                tempEffects += effect.InstructionText + " ";
            }
        }

        InstructionText += tempEffects + " " + permanentEffects;
    }
}