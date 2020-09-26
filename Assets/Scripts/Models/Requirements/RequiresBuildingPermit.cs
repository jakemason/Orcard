using UnityEngine;

/// <summary>
/// Making this a ScriptableObject is clunky, but it allows us to easily set
/// these through the inspector interface and allows us to mix and match them
/// with other CastingRequirements.
/// </summary>
[CreateAssetMenu(fileName = "New Requires Building Permit", menuName = "Requirements/Requires Building Permit")]
public class RequiresBuildingPermit : Requirement
{
    public override bool RequirementMet()
    {
        return BuildingManager.BuildingPermitsUsed < BuildingManager.BuildingPermitsAvailable;
    }
}