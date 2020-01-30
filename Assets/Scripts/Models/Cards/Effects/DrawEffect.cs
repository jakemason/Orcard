using Players;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Draw Effect", menuName = "Effects/Draw Effect")]
public class DrawEffect : Effect
{
    public int CardsToDraw = 1;

    public void OnValidate()
    {
        InstructionText = $"Draw {CardsToDraw} Cards.";
        string assetPath = AssetDatabase.GetAssetPath(GetInstanceID());
        AssetDatabase.RenameAsset(assetPath, InstructionText);
    }

    public override void Activate()
    {
        Player.Instance.Draw(CardsToDraw);
    }
}