using System;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card Cost Effect", menuName = "Effects/Card Cost Effect")]
public class CardCostEffect : Effect
{
    public int CardCostModifier = -1;

    public enum Type
    {
        Temp,
        Permanent
    }

    public Type EffectDuration = Type.Temp;

    public override void Activate()
    {
        CardRenderer card = SpellCast.Target as CardRenderer;

        if (card == null) return;

        card.CardObject.CastingCost += CardCostModifier;
        if (EffectDuration == Type.Permanent)
        {
            card.gameObject.GetComponent<PlayableCardController>().OriginalCard.CastingCost += CardCostModifier;
        }

        card.UpdateCardDetails();
    }

#if UNITY_EDITOR
    public void OnValidate()
    {
        InstructionText = CardCostModifier > 0
            ? $"Increase a random card's cost by {CardCostModifier}"
            : $"Reduce a random card's cost by {CardCostModifier}";

        switch (EffectDuration)
        {
            case Type.Temp:
                InstructionText += " temporarily.";
                break;
            case Type.Permanent:
                InstructionText += " permanently.";
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        string assetPath = AssetDatabase.GetAssetPath(GetInstanceID());
        AssetDatabase.RenameAsset(assetPath, InstructionText);
    }
#endif
}