using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Reload Effect", menuName = "Effects/Reload Effect")]
public class ReloadEffect : Effect
{
#if UNITY_EDITOR
    public void OnValidate()
    {
        InstructionText = "Reload target tower.";
        string assetPath = AssetDatabase.GetAssetPath(GetInstanceID());
        AssetDatabase.RenameAsset(assetPath, InstructionText);
    }
#endif

    public override void Activate()
    {
        Tower tower = SpellCast.Target as Tower;
        if (tower != null) tower.Reload();
    }
}