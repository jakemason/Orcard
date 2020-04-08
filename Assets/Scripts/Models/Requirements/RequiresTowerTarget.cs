using UnityEngine;

[CreateAssetMenu(fileName = "New Requires Tower Target", menuName = "Requirements/Requires Tower Target")]
public class RequiresTowerTarget : Requirement
{
    public override bool RequirementMet()
    {
        return (Tower) SpellCast.Target != null;
    }
}