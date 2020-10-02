using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Target Random Card Effect", menuName = "Effects/Target Random Card Effect")]
public class TargetRandomCardInHand : Effect
{
    public override void Activate()
    {
        if (PlayerHand.Instance.HeldCards.Count == 0)
        {
            SpellCast.CardTarget = null;
            return;
        }


        List<PlayableCardController> cardsInHand = PlayerHand.Instance.GetHeldCardsControllers();
        int                          index       = Random.Range(0, cardsInHand.Count);
        SpellCast.CardTarget = cardsInHand[index];
        Debug.Log($"We are randomly targeting {SpellCast.CardTarget} in the player's hand.");
    }

    public override void Deactivate()
    {
    }
}