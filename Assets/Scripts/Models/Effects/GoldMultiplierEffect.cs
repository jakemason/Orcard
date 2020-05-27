using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gold Multiplier Effect", menuName = "Effects/Gold Multiplier Effect")]
public class GoldMultiplierEffect : Effect
{
    public int Multiplier = 1;

    public override void Activate()
    {
        IncomeController.SetMultiplier(Multiplier);
    }

#if UNITY_EDITOR
    public void OnValidate()
    {
        InstructionText = $"{Multiplier}X on your next income card.";
        string assetPath = AssetDatabase.GetAssetPath(GetInstanceID());
        AssetDatabase.RenameAsset(assetPath, InstructionText);
    }
#endif
}