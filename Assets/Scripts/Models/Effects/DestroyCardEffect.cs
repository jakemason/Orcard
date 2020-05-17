using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Destroy Card Effect", menuName = "Effects/Destroy Card Effect")]
public class DestroyCardEffect : Effect
{
    public bool DestroyHand;
    public int CardsToDestroy = 1;

#if UNITY_EDITOR
    public void OnValidate()
    {
        InstructionText = DestroyHand ? "Destroy the cards in your hand." : $"Destroy {CardsToDestroy} Cards.";
        string assetPath = AssetDatabase.GetAssetPath(GetInstanceID());
        AssetDatabase.RenameAsset(assetPath, InstructionText);
    }
#endif

    public override void Activate()
    {
        List<PlayableCardController> toDestroy = new List<PlayableCardController>();
        if (DestroyHand)
        {
            for (int i = PlayerHand.Instance.HeldCards.Count - 1; i >= 0; i--)
            {
                PlayerHand.DestroyCard(PlayerHand.Instance.HeldCards[i].GetComponent<PlayableCardController>());
            }
        }
        else
        {
        }
    }
}