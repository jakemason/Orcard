using UnityEngine;

[CreateAssetMenu(fileName = "New Construction Effect", menuName = "Construction/Construction Effect")]
public class ConstructionEffect : Effect
{
    public GameObject ConstructionPrefab;

    public override void Activate()
    {
        GameObject go    = Instantiate(ConstructionPrefab, SpellCast.CastPosition, Quaternion.identity);
        Tower      tower = go.GetComponent<Tower>();
        TowerCard  card  = (TowerCard) SpellCast.AttemptingToCast;
        tower.Model = card;
    }
}