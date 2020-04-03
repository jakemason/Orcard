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


        BasicInspector basic = gameObject.AddComponent<BasicInspector>();
        basic.Model = CardReference.CardObject;
    }
}