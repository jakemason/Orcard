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
        //We need to use a copy here because Upgrade cards alter the stats of the model throughout gameplay
        tower.Model = Instantiate(card);
        TowerManager.Instance.ConstructedTowers.Add(SpellCast.CastPosition, tower);
    }

    private void OnValidate()
    {
        InstructionText = "Build this tower.";
    }
}