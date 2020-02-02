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
        InstructionText = $"Draw {CardsToDraw} Cards.";
        string assetPath = AssetDatabase.GetAssetPath(GetInstanceID());
        AssetDatabase.RenameAsset(assetPath, InstructionText);
    }
#endif

    public override void Activate()
    {
        Debug.Log("Draw Effect Fired.");
        Player.Instance.Draw(CardsToDraw);
    }
}