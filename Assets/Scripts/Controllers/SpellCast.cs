using Players;
using UnityEngine;

public static class SpellCast
{
    public static ITargetable Target = null;
    public static PlayableCardController LastCardPlayed;
    public static Card AttemptingToCast = null;
    public static Vector2 CastPosition;

    public static void SetTarget(ITargetable target)
    {
        Target = target;
    }

    public static void ClearTarget()
    {
        Target = null;
    }

    public static void ClearCard()
    {
        AttemptingToCast = null;
    }

    public static void Clear()
    {
        ClearCard();
        ClearTarget();
    }

    public static void Resolve()
    {
        foreach (Effect playEffect in AttemptingToCast.ImmediateEffects)
        {
            playEffect.Activate();
        }

        SpellCard spellCard = AttemptingToCast as SpellCard;
        if (spellCard != null)
        {
            foreach (Effect effect in spellCard.PermanentStartOfTurnEffects)
            {
                PlayerController.RegisterPermanentOnTurnEffect(effect);
            }

            foreach (Effect effect in spellCard.StartOfNextTurnEffects)
            {
                PlayerController.RegisterTemporaryOnTurnEffect(effect);
            }
        }

        PlayerController.Instance.RemainingEnergy -= AttemptingToCast.CastingCost;
        TargetIndicator.Disable();
        Clear();
    }

    public static bool CastingRequirementsMet()
    {
        if (PlayerController.Instance.RemainingEnergy < AttemptingToCast.CastingCost)
        {
            return false;
        }

        foreach (Requirement requirement in AttemptingToCast.CastingRequirements)
        {
            if (!requirement.RequirementMet())
            {
                return false;
            }
        }

        return true;
    }
}