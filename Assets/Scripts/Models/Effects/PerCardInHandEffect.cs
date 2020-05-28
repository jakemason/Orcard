using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Per Card In Hand Effect", menuName = "Effects/Per Card In Hand Effect")]
public class PerCardInHandEffect : Effect
{
    public Card CardToCount;
    public List<Effect> EffectsPerCardInHand;

    public override void Activate()
    {
        foreach (Card card in PlayerHand.Instance.GetHeldCardsData())
        {
            //TODO: string comparison, not cool....use IDs?
            if (card.Name == null || card.Name != CardToCount.Name) continue;
            Debug.Log("Fire");
            foreach (Effect effect in EffectsPerCardInHand)
            {
                effect.Activate();
            }
        }
    }

    public override void Deactivate()
    {
    }
#if UNITY_EDITOR
    public void OnValidate()
    {
        InstructionText = "For each other " + CardToCount.Name + " in hand, ";
        foreach (Effect effect in EffectsPerCardInHand)
        {
            InstructionText += effect.InstructionText;
        }

        string assetPath = AssetDatabase.GetAssetPath(GetInstanceID());
        AssetDatabase.RenameAsset(assetPath, InstructionText);
    }
#endif
}