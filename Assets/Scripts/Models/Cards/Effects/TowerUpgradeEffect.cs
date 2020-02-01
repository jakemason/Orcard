using System;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tower Upgrade Effect", menuName = "Effects/Tower Upgrade Effect")]
public class TowerUpgradeEffect : Effect
{
    public float Modifier = 1.0f;
    public TowerAttribute AttributeToAlter;

    public enum TowerAttribute
    {
        Attack,
        AttackRate
    }

    public override void Activate()
    {
        Tower tower = SpellCast.Target as Tower;
        if (!tower) return;

        switch (AttributeToAlter)
        {
            case TowerAttribute.Attack:
                tower.Model.Damage += (int) Modifier;
                break;
            case TowerAttribute.AttackRate:
                tower.Model.AttackRate += Modifier;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        tower.Model.UpdateComputedValues();
    }

#if UNITY_EDITOR
    public void OnValidate()
    {
        switch (AttributeToAlter)
        {
            case TowerAttribute.Attack:
                InstructionText = Modifier > 0 ? $"+{Modifier} Tower Attack." : $"{Modifier} Tower Attack.";
                break;
            case TowerAttribute.AttackRate:
                InstructionText = Modifier > 0 ? $"+{Modifier} Tower Attack Rate." : $"{Modifier}  Tower Attack Rate.";
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        string assetPath = AssetDatabase.GetAssetPath(GetInstanceID());
        AssetDatabase.RenameAsset(assetPath, InstructionText);
    }
#endif
}