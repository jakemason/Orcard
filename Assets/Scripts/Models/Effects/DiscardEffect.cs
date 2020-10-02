using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Discard Effect", menuName = "Effects/Discard Effect")]
public class DiscardEffect : Effect
{
    public int CardsToDiscard = 1;

#if UNITY_EDITOR
    public void OnValidate()
    {
        InstructionText = $"Discard {CardsToDiscard} Cards.";
        string assetPath = AssetDatabase.GetAssetPath(GetInstanceID());
        AssetDatabase.RenameAsset(assetPath, InstructionText);
    }
#endif

    public override void Activate()
    {
        PlayableCardController card = SpellCast.CardTarget as PlayableCardController;
        if (card == null) return;

        Debug.Log($"Discarding {card.CardObject.Name}");
        PlayerHand.DiscardCard(card);
    }

    public override void Deactivate()
    {
    }
}