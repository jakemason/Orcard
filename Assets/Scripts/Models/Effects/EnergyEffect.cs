using Players;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Energy Effect", menuName = "Effects/Energy Effect")]
public class EnergyEffect : Effect
{
    public int EnergyModifier = 1;

    public override void Activate()
    {
        PlayerController.Instance.RemainingEnergy += EnergyModifier;
    }
#if UNITY_EDITOR
    public void OnValidate()
    {
        InstructionText = EnergyModifier > 0 ? $"+{EnergyModifier} Energy." : $"-{EnergyModifier} Energy.";
        string assetPath = AssetDatabase.GetAssetPath(GetInstanceID());
        AssetDatabase.RenameAsset(assetPath, InstructionText);
    }
#endif
}