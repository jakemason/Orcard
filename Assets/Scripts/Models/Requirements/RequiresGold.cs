using Players;
using UnityEngine;

[CreateAssetMenu(fileName = "New Requires Gold Requirement", menuName = "Requirements/Requires Gold Requirement")]
public class RequiresGold : Requirement
{
    public int GoldRequirement = 0;

    public override bool RequirementMet()
    {
        return Player.Instance.CurrentGold >= GoldRequirement;
    }
}