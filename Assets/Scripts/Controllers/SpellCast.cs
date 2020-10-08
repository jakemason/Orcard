using Players;
using UnityEngine;

public static class SpellCast
{
    public static ITargetable Target = null;
    public static ITargetable CardTarget = null;
    public static PlayableCardController LastCardPlayed;
    public static bool AwaitingCardTarget;
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

        IncomeController.ModifyGold(-AttemptingToCast.CastingCost);
        //PlayerController.Instance.RemainingEnergy -= AttemptingToCast.CastingCost;
        TargetIndicator.Disable();
        Clear();
    }

    public static bool CastingRequirementsMet()
    {
        if (IncomeController.GetCurrentGold() < AttemptingToCast.CastingCost)
        {
            Debug.Log("Need more cash to do that");
            PlayOneShotSound.Play(SoundLibrary.Global.ErrorSound, 0.8f, 1.0f);
            ClosedCaptioning.CreateMessage("Not enough gold!");
            return false;
        }

        foreach (Requirement requirement in AttemptingToCast.CastingRequirements)
        {
            if (!requirement.RequirementMet())
            {
                Debug.Log("Requirement: " + requirement.name + " not met.");
                PlayOneShotSound.Play(SoundLibrary.Global.ErrorSound, 0.8f, 1.0f);
                ClosedCaptioning.CreateMessage(requirement.RequirementFailMessage);
                return false;
            }
        }

        return true;
    }
}