using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Per Building Effect", menuName = "Effects/Per Building Effect")]
public class PerBuildingEffect : Effect
{
    public BuildingCard BuildingToCount;
    public List<Effect> EffectsPerBuilding;

    public override void Activate()
    {
        foreach (KeyValuePair<Vector2, Building> valuePair in BuildingManager.GetBuildings())
        {
            //TODO: This valuePair.Value.Model.Name != BuildingToCount.Name,
            //TODO: should we generate a number from the string for each card via OnValidate()?
            if (valuePair.Value.Model == null || valuePair.Value.Model.Name != BuildingToCount.Name) continue;

            foreach (Effect effect in EffectsPerBuilding)
            {
                effect.Activate();
            }
        }
    }

    public override void Deactivate()
    {
    }
#if UNITY_EDITOR
    public void OnValidate()
    {
        InstructionText = "For each " + BuildingToCount.Name + ", ";
        foreach (Effect effect in EffectsPerBuilding)
        {
            InstructionText += effect.InstructionText;
        }

        string assetPath = AssetDatabase.GetAssetPath(GetInstanceID());
        AssetDatabase.RenameAsset(assetPath, InstructionText);
    }
#endif
}