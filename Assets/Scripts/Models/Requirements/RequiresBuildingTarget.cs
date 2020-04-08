using UnityEngine;

[CreateAssetMenu(fileName = "New Requires Building Target", menuName = "Requirements/Requires Building Target")]
public class RequiresBuildingTarget : Requirement
{
    public override bool RequirementMet()
    {
        return (Building) SpellCast.Target != null;
    }
}