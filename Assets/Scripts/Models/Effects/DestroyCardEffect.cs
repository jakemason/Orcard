using System.Collections.Generic;
using Players;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Destroy Card Effect", menuName = "Effects/Destroy Card Effect")]
public class DestroyCardEffect : Effect
{
    public bool DestroyHand;
    public bool DestroyDeck;
    public bool DestroyAllOfThem;

#if UNITY_EDITOR
    public void OnValidate()
    {
        InstructionText = DestroyHand ? "Destroy the cards in your hand." : "Destroy Target Card.";
        if (DestroyAllOfThem)
        {
            InstructionText = "Destroy target card and remove all copies of it from your deck.";
        }

        if (DestroyDeck)
        {
            InstructionText = "Destroy your deck.";
        }

        if (DestroyDeck && DestroyHand)
        {
            InstructionText = "Destroy your hand and deck.";
        }

        string assetPath = AssetDatabase.GetAssetPath(GetInstanceID());
        AssetDatabase.RenameAsset(assetPath, InstructionText);
    }
#endif

    public override void Activate()
    {
        if (DestroyHand)
        {
            for (int i = PlayerHand.Instance.HeldCards.Count - 1; i >= 0; i--)
            {
                PlayerHand.DestroyCardInHand(PlayerHand.Instance.HeldCards[i].GetComponent<PlayableCardController>());
            }
        }

        if (DestroyDeck)
        {
            PlayerController.Instance.DeckForCurrentRun.Cards.Clear();
            PlayerController.Instance.DiscardPile.Clear();
        }

        if (DestroyAllOfThem)
        {
            PlayableCardController target = SpellCast.CardTarget as PlayableCardController;
            if (target == null) return;

            PlayerController.Instance.DeckForCurrentRun.Cards.RemoveAll(x => x.Name == target.CardObject.Name);
            PlayerController.Instance.DiscardPile.RemoveAll(x => x.Name             == target.CardObject.Name);

            for (int i = PlayerHand.Instance.HeldCards.Count - 1; i >= 0; i--)
            {
                PlayableCardController cardInHand =
                    PlayerHand.Instance.HeldCards[i].GetComponent<PlayableCardController>();
                if (target.CardObject.Name == cardInHand.CardObject.Name)
                {
                    PlayerHand.DestroyCardInHand(cardInHand);
                }
            }
        }
        else
        {
            PlayableCardController card = SpellCast.CardTarget as PlayableCardController;
            if (card == null) return;
            PlayerHand.DestroyCardInHand(card);
        }
    }

    public override void Deactivate()
    {
    }
}