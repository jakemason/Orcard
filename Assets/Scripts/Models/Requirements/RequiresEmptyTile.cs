using UnityEngine;

/// <summary>
/// Making this a ScriptableObject is clunky, but it allows us to easily set
/// these through the inspector interface and allows us to mix and match them
/// with other CastingRequirements.
/// </summary>
[CreateAssetMenu(fileName = "New Requires Empty Tile", menuName = "Requirements/Requires Empty Tile")]
public class RequiresEmptyTile : Requirement
{
    public override bool RequirementMet()
    {
        bool requirementMet = SpellCast.Target == null;
        if (MapController.IsOccupied(SpellCast.CastPosition))
        {
            requirementMet = false;
        }

        return requirementMet;
    }
}