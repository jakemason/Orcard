using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Building Permit Effect", menuName = "Effects/Building Permit Effect")]
public class AddBuildingPermitEffect : Effect
{
    public int PermitModifier = 1;

    public override void Activate()
    {
        BuildingManager.BuildingPermitsAvailable += PermitModifier;
    }

    public override void Deactivate()
    {
    }

#if UNITY_EDITOR
    public void OnValidate()
    {
        string noun = "Permit";
        if (PermitModifier > 1 || PermitModifier < -1)
        {
            noun += "s";
        }

        InstructionText = PermitModifier > 0
            ? $"+{PermitModifier} Building {noun}."
            : $"-{PermitModifier} Building {noun}.";
        string assetPath = AssetDatabase.GetAssetPath(GetInstanceID());
        AssetDatabase.RenameAsset(assetPath, InstructionText);
    }
#endif
}