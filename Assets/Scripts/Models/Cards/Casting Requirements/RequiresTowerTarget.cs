using UnityEngine;

/// <summary>
/// Making this a ScriptableObject is clunky, but it allows us to easily set
/// these through the inspector interface and allows us to mix and match them
/// with other CastingRequirements.
///
/// TODO: Probably want to rename this to RequiresUnitTarget as we'll also want Cards to be targetable
/// TODO: (Used in things like "Discard / Destroy target card", etc, etc
/// </summary>
[CreateAssetMenu(fileName = "New Requires Tower Target", menuName = "Card Requirements/Requires Tower Target")]
public class RequiresTowerTarget : CastingRequirement
{
    public override bool RequirementMet()
    {
        bool requirementMet = (Tower) SpellCast.Target != null;

        return requirementMet;
    }
}