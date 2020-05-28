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
        CardRenderer card = SpellCast.Target as CardRenderer;
        if (card == null) return;

        PlayableCardController playableCard = card.gameObject.GetComponent<PlayableCardController>();
        if (playableCard == null) return;

        PlayerHand.DiscardCard(playableCard);
    }

    public override void Deactivate()
    {
    }
}