using UnityEngine;

[CreateAssetMenu(fileName = "New Clearing Effect", menuName = "Effects/Clearing Effect")]
public class ClearEffect : Effect
{
    public override void Activate()
    {
        Building target = BuildingManager.GetBuildingAt(SpellCast.CastPosition);
        if (target != null)
        {
            target.DestroyBuilding();
        }
    }

    private void OnValidate()
    {
        InstructionText = "Destroy target building.";
    }
}