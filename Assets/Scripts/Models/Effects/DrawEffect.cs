using Players;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Draw Effect", menuName = "Effects/Draw Effect")]
public class DrawEffect : Effect
{
    public int CardsToDraw = 1;

#if UNITY_EDITOR
    public void OnValidate()
    {
        InstructionText = $"Draw {CardsToDraw} Card";
        if (CardsToDraw > 1)
        {
            InstructionText += "s";
        }

        InstructionText += ".";
        string assetPath = AssetDatabase.GetAssetPath(GetInstanceID());
        AssetDatabase.RenameAsset(assetPath, InstructionText);
    }
#endif

    public override void Activate()
    {
        Player.Instance.Draw(CardsToDraw);
    }
}