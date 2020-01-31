using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemies/Enemy")]
public class Enemy : ScriptableObject
{
    public string Name;
    public float Speed = 1.0f;
    public int HP;
    [Range(0.5f, 1.5f)] public float MinSize = 1.0f;
    [Range(0.5f, 1.5f)] public float MaxSize = 1.0f;
    public Sprite Sprite;
    public int Damage;


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