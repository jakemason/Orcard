using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Deck", menuName = "Deck")]
public class Deck : ScriptableObject
{
    public string Name;
    public List<Card> Cards;

#if UNITY_EDITOR
    private void OnValidate()
    {
        // this makes the editor typing experience feel a little weird, but saves us from having to manually
        // rename the file to match the name of the card. I feel this is useful enough to keep for now.
        if (Name != "")
        {
            string assetPath = AssetDatabase.GetAssetPath(GetInstanceID());
            AssetDatabase.RenameAsset(assetPath, Name);
        }
    }
#endif
}