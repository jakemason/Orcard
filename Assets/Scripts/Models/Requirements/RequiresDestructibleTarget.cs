using UnityEngine;

[CreateAssetMenu(fileName = "New Requires Destructible Building Target",
    menuName              = "Requirements/Requires Destructible Target")]
public class RequiresDestructibleTarget : Requirement
{
    public override bool RequirementMet()
    {
        Building b = (Building) SpellCast.Target;
        return b != null && !b.IsIndestructable;
    }
}