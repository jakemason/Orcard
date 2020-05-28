using UnityEngine;

[CreateAssetMenu(fileName = "New Clearing Effect", menuName = "Effects/Clearing Effect")]
public class ClearEffect : Effect
{
    public GameObject ToolsPrefab;

    public override void Activate()
    {
        Vector2    pos    = new Vector2((int) SpellCast.CastPosition.x, (int) SpellCast.CastPosition.y);
        GameObject tools  = Instantiate(ToolsPrefab, pos, Quaternion.identity);
        Building   target = BuildingManager.GetBuildingAt(pos);
        if (target != null && !target.IsIndestructable)
        {
            BuildingManager.RemoveBuilding(pos);
            target.DestroyBuilding();
            target.OnDeconstruction();
        }
    }

    public override void Deactivate()
    {
        throw new System.NotImplementedException();
    }

    private void OnValidate()
    {
        InstructionText = "Remove target boulder.";
    }
}