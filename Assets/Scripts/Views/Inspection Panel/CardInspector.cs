using UnityEngine;

public class CardInspector : MonoBehaviour
{
    public CardRenderer CardReference;

    private void Start()
    {
        TowerCard towerCast = CardReference.CardObject as TowerCard;
        if (towerCast != null)
        {
            TowerInspector inspector = gameObject.AddComponent<TowerInspector>();
            inspector.Model = towerCast;
            return;
        }


        BuildingCard buildingCast = CardReference.CardObject as BuildingCard;
        if (buildingCast != null)
        {
            BuildingInspector inspector = gameObject.AddComponent<BuildingInspector>();
            inspector.Model = buildingCast;
            return;
        }
    }
}