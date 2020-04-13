using UnityEngine;

[CreateAssetMenu(fileName = "New Clearing Effect", menuName = "Effects/Clearing Effect")]
public class ClearEffect : Effect
{
    public override void Activate()
    {
        Vector2  pos    = new Vector2((int) SpellCast.CastPosition.x, (int) SpellCast.CastPosition.y);
        Building target = BuildingManager.GetBuildingAt(pos);
        if (target != null && !target.IsIndestructable)
        {
            BuildingManager.Instance.ConstructedBuildings.Remove(pos);
            target.DestroyBuilding();
        }
    }

    private void OnValidate()
    {
        InstructionText = "Destroy target building.";
    }
}