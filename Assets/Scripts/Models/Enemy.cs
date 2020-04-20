using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemies/Enemy")]
public class Enemy : ScriptableObject
{
    // @formatter:off 
    public string Name;
    public float Speed = 1.0f;
    public int HP;
    [Range(0.5f, 1.5f)] public float MinSize = 1.0f;
    [Range(0.5f, 1.5f)] public float MaxSize = 1.0f;
    public int Damage;
    [TextArea] public string FlavorText;
    
    [Header("Computed Values")]
    [Space(20)]
    private static float _speedValueWeight = 1.0f;
    private static float _damageValueWeight = 1.0f;
    private static float _hpValueWeight = 1.0f;
    
    [Header("Accessories")]
    public bool HasSword;
    public bool HasSmallHelm;
    public bool HasLargeHelm;
    public bool HasShield;
    public bool HasBoots;
    public bool HasSmallShoulders;
    public bool HasLargeShoulders;
    public bool HasWarBanner;
    
    [ReadOnly] public float OverallStrength = 1.0f;
    // @formatter:on 


#if UNITY_EDITOR
    private void OnValidate()
    {
        OverallStrength = (Speed * _speedValueWeight) + (Damage * _damageValueWeight) + (HP * _hpValueWeight);
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