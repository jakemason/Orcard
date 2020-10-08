using Players;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Requires Gold Requirement", menuName = "Requirements/Requires Gold Requirement")]
public class RequiresGold : Requirement
{
    public int GoldRequirement = 0;

    public override bool RequirementMet()
    {
        return IncomeController.GetCurrentGold() >= GoldRequirement;
    }

#if UNITY_EDITOR
    public void OnValidate()
    {
        RequirementFailMessage = $"You don't have enough gold. {GoldRequirement} gold needed.";
    }
#endif
}