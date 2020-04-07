using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Health Effect", menuName = "Effects/Health Effect")]
public class HealthEffect : Effect
{
    public int HealthModifier = 1;

    public override void Activate()
    {
        Core.Instance.Health += HealthModifier;
    }

#if UNITY_EDITOR
    public void OnValidate()
    {
        InstructionText = HealthModifier > 0 ? $"+{HealthModifier} HP." : $"{HealthModifier} HP.";
        string assetPath = AssetDatabase.GetAssetPath(GetInstanceID());
        AssetDatabase.RenameAsset(assetPath, InstructionText);
    }
#endif
}