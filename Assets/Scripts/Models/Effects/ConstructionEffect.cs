using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Construction Effect", menuName = "Effects/Construction Effect")]
public class ConstructionEffect : Effect
{
    public GameObject ConstructionPrefab;

    public override void Activate()
    {
        //TODO: Maybe just two different Construction Effects with separate Prefabs would be easiest?
        GameObject     go        = Instantiate(ConstructionPrefab, SpellCast.CastPosition, Quaternion.identity);
        SpriteRenderer rend      = go.GetComponent<SpriteRenderer>();
        TowerCard      towerCard = SpellCast.AttemptingToCast as TowerCard;
        if (towerCard != null)
        {
            Tower     tower = go.AddComponent<Tower>();
            TowerCard card  = (TowerCard) SpellCast.AttemptingToCast;
            rend.sprite       = card.Artwork;
            rend.sortingOrder = (int) -go.transform.position.y;
            rend.color        = card.Tint;
            //We need to use a copy here because Upgrade cards alter the stats of the model throughout gameplay
            tower.Model = Instantiate(card);
            BuildingManager.Instance.ConstructedBuildings.Add(SpellCast.CastPosition, tower);
            TowerInspector inspector = go.transform.GetChild(0).gameObject.AddComponent<TowerInspector>();
            inspector.TowerReference = tower;
        }
        else
        {
            Building     building = go.AddComponent<Building>();
            BuildingCard card     = (BuildingCard) SpellCast.AttemptingToCast;
            rend.sprite       = card.Artwork;
            rend.sortingOrder = (int) -go.transform.position.y;
            rend.color        = card.Tint;
            //We need to use a copy here because Upgrade cards alter the stats of the model throughout gameplay
            building.Model = Instantiate(card);
            BuildingManager.Instance.ConstructedBuildings.Add(SpellCast.CastPosition, building);
            BasicInspector inspector = go.transform.GetChild(0).gameObject.AddComponent<BasicInspector>();
            inspector.BuildingReference = building;
        }
    }

    private void OnValidate()
    {
        InstructionText = "";
    }
}