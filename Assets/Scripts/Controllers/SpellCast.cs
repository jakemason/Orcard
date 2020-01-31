using Players;
using UnityEngine;

public static class SpellCast
{
    public static ITargetable Target = null;
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

    public static bool HasValidTarget()
    {
        return Target != null;
    }

    public static void SetCastingCard(Card wantToCast)
    {
        AttemptingToCast = wantToCast;
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
        foreach (Effect playEffect in AttemptingToCast.PlayEffects)
        {
            playEffect.Activate();
        }

        Player.Instance.RemainingEnergy -= AttemptingToCast.CastingCost;
        ConstructionIndicator.Disable();
        //Player.Instance.TargettingIndicator.SetActive(false);
        Clear();
    }

    public static bool CastingRequirementsMet()
    {
        if (Player.Instance.RemainingEnergy < AttemptingToCast.CastingCost)
        {
            return false;
        }

        foreach (CastingRequirement requirement in AttemptingToCast.CastingRequirements)
        {
            if (!requirement.RequirementMet())
            {
                return false;
            }
        }

        return true;
    }
}