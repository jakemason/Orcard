using UnityEngine;

[CreateAssetMenu(fileName = "New Construction Effect", menuName = "Effects/Construction Effect")]
public class ConstructionEffect : Effect
{
    public GameObject ConstructionPrefab;
    public GameObject ToolsPrefab;

    public override void Activate()
    {
        //TODO: Maybe just two different Construction Effects with separate Prefabs would be easiest?
        GameObject     go        = Instantiate(ConstructionPrefab, SpellCast.CastPosition, Quaternion.identity);
        Animator       animator  = go.GetComponent<Animator>();
        GameObject     tools     = Instantiate(ToolsPrefab, SpellCast.CastPosition, Quaternion.identity);
        SpriteRenderer rend      = go.GetComponent<SpriteRenderer>();
        TowerCard      towerCard = SpellCast.AttemptingToCast as TowerCard;

        SpriteRenderer shadowRend = go.GetComponentsInChildren<SpriteRenderer>()[1]; //jump the parent

        if (towerCard != null)
        {
            Tower     tower = go.AddComponent<Tower>();
            TowerCard card  = (TowerCard) SpellCast.AttemptingToCast;
            go.name           = card.name;
            shadowRend.sprite = towerCard.Shadow;
            Debug.Log(towerCard.Shadow);
            if (towerCard.Name != null)
            {
                animator.runtimeAnimatorController =
                    Resources.Load("Visuals/Animations/Buildings/" + towerCard.Name) as RuntimeAnimatorController;
            }

            tower.IsIndestructable = card.IsIndestructible;
            rend.sprite            = card.Artwork;
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
            go.name           = card.name;
            shadowRend.sprite = card.Shadow;
            if (card.Name != null)
            {
                animator.runtimeAnimatorController =
                    Resources.Load("Visuals/Animations/Buildings/" + card.Name) as RuntimeAnimatorController;
            }

            building.IsIndestructable = card.IsIndestructible;
            rend.sprite               = card.Artwork;
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