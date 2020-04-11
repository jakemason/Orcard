using Players;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gold Modifier Effect", menuName = "Effects/Gold Modifier Effect")]
public class GoldEffect : Effect
{
    public int GoldModifier = 1;

    public override void Activate()
    {
        IncomeController.ModifyGold(GoldModifier);
    }

#if UNITY_EDITOR
    public void OnValidate()
    {
        InstructionText = GoldModifier > 0 ? $"+{GoldModifier} Gold." : $"{GoldModifier} Gold.";
        string assetPath = AssetDatabase.GetAssetPath(GetInstanceID());
        AssetDatabase.RenameAsset(assetPath, InstructionText);
    }
#endif
}