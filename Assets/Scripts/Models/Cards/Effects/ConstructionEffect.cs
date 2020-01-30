using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Construction Effect", menuName = "Effects/Construction Effect")]
public class ConstructionEffect : Effect
{
    public GameObject ConstructionPrefab;

    public override void Activate()
    {
        GameObject     go    = Instantiate(ConstructionPrefab, SpellCast.CastPosition, Quaternion.identity);
        Tower          tower = go.GetComponent<Tower>();
        TowerCard      card  = (TowerCard) SpellCast.AttemptingToCast;
        SpriteRenderer rend  = go.GetComponent<SpriteRenderer>();
        rend.sprite = card.Artwork;
        rend.color  = card.Tint;
        tower.Model = card;
        TowerManager.Instance.ConstructedTowers.Add(tower);
    }

    private void OnValidate()
    {
        InstructionText = "Build this tower.";
    }
}