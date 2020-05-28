using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Adjacency Tower Effect", menuName = "Effects/Adjacency Tower Effect")]
public class AdjacentBuildingModifier : Effect
{
    public enum AttributeToModify
    {
        Damage,
        AttackRate
    }

    public int Modifier;

    public AttributeToModify ToModify;

    public override void Activate()
    {
        Vector2       position         = BuildingManager.LastPositionTouched;
        List<Vector2> positionsToCheck = new List<Vector2>();
        positionsToCheck.Add(position                + Vector2.up);    // N
        positionsToCheck.Add(position + Vector2.up   + Vector2.left);  // NW
        positionsToCheck.Add(position + Vector2.up   + Vector2.right); // NE
        positionsToCheck.Add(position                + Vector2.down);  // S
        positionsToCheck.Add(position + Vector2.down + Vector2.left);  // SW
        positionsToCheck.Add(position + Vector2.down + Vector2.right); // SE
        positionsToCheck.Add(position                + Vector2.right); // E
        positionsToCheck.Add(position                + Vector2.left);  // W

        foreach (Vector2 pos in positionsToCheck)
        {
            Tower t = BuildingManager.GetBuildingAt(pos) as Tower;
            if (t != null && !t.AffectedBy.Contains(BuildingManager.GetBuildingAt(position)))
            {
                switch (ToModify)
                {
                    case AttributeToModify.Damage:
                        t.Model.Damage += Modifier;
                        break;
                    case AttributeToModify.AttackRate:
                        t.Model.AttackRate += Modifier;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                t.AffectedBy.Add(BuildingManager.GetBuildingAt(position));
            }
        }
    }

    public override void Deactivate()
    {
        Vector2       position         = BuildingManager.LastPositionTouched;
        List<Vector2> positionsToCheck = new List<Vector2>();
        positionsToCheck.Add(position                + Vector2.up);    // N
        positionsToCheck.Add(position + Vector2.up   + Vector2.left);  // NW
        positionsToCheck.Add(position + Vector2.up   + Vector2.right); // NE
        positionsToCheck.Add(position                + Vector2.down);  // S
        positionsToCheck.Add(position + Vector2.down + Vector2.left);  // SW
        positionsToCheck.Add(position + Vector2.down + Vector2.right); // SE
        positionsToCheck.Add(position                + Vector2.right); // E
        positionsToCheck.Add(position                + Vector2.left);  // W

        foreach (Vector2 pos in positionsToCheck)
        {
            Tower t = BuildingManager.GetBuildingAt(pos) as Tower;
            if (t != null)
            {
                switch (ToModify)
                {
                    case AttributeToModify.Damage:
                        t.Model.Damage -= Modifier;
                        break;
                    case AttributeToModify.AttackRate:
                        t.Model.AttackRate -= Modifier;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                t.AffectedBy.Remove(BuildingManager.GetBuildingAt(position));
            }
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        char symbol = Modifier > 0 ? '+' : '-';
        switch (ToModify)
        {
            case AttributeToModify.Damage:
                InstructionText = $"For adjacent towers, {symbol}{Modifier} Damage.";
                break;
            case AttributeToModify.AttackRate:
                InstructionText = $"For adjacent towers, {symbol}{Modifier} Attack Rate.";
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
#endif
}