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

        int index = Random.Range(0, PlayerHand.Instance.HeldCards.Count);
        SpellCast.CardTarget = PlayerHand.Instance.HeldCards[index].GetComponent<PlayableCardController>();
    }

    public override void Deactivate()
    {
    }
}