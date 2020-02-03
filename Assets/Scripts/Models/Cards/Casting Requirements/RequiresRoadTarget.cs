using UnityEngine;

/// <summary>
/// Making this a ScriptableObject is clunky, but it allows us to easily set
/// these through the inspector interface and allows us to mix and match them
/// with other CastingRequirements.
/// </summary>
[CreateAssetMenu(fileName = "New Requires Road Target", menuName = "Card Requirements/Requires Road Target")]
public class RequiresRoadTarget : CastingRequirement
{
    public override bool RequirementMet()
    {
        return MapController.IsOccupied(SpellCast.CastPosition);
    }
}