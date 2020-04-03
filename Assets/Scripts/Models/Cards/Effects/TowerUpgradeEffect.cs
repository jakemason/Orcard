using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tower Upgrade Effect", menuName = "Effects/Tower Upgrade Effect")]
public class TowerUpgradeEffect : Effect
{
    public enum TargetType
    {
        Single,
        All
    }

    public float Modifier = 1.0f;
    public TowerAttribute AttributeToAlter;
    public TargetType Target = TargetType.Single;

    public enum TowerAttribute
    {
        Attack,
        AttackRate
    }

    public override void Activate()
    {
        switch (Target)
        {
            case TargetType.Single:
                Tower tower = SpellCast.Target as Tower;
                UpgradeSingleTower(tower);
                break;
            case TargetType.All:
                foreach (KeyValuePair<Vector2, Building> valuePair in BuildingManager.Instance.ConstructedBuildings)
                {
                    Tower towerCast = valuePair.Value as Tower;
                    if (towerCast != null)
                    {
                        UpgradeSingleTower(towerCast);
                    }
                }

                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void UpgradeSingleTower(Tower tower)
    {
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

        switch (Target)
        {
            case TargetType.Single:
                break;
            case TargetType.All:
                InstructionText += " Affects all towers.";
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        string assetPath = AssetDatabase.GetAssetPath(GetInstanceID());
        AssetDatabase.RenameAsset(assetPath, InstructionText);
    }
#endif
}