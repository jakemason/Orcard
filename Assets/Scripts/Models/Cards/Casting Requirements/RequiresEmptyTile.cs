using UnityEngine;

/// <summary>
/// Making this a ScriptableObject is clunky, but it allows us to easily set
/// these through the inspector interface and allows us to mix and match them
/// with other CastingRequirements.
/// </summary>
[CreateAssetMenu(fileName = "New Requires Empty Tile", menuName = "Card Requirements/Requires Empty Tile")]
public class RequiresEmptyTile : CastingRequirement
{
    public override bool RequirementMet()
    {
        bool requirementMet = SpellCast.Target == null;

        return requirementMet;
    }
}