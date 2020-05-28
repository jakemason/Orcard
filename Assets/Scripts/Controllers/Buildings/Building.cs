using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class Building : MonoBehaviour, ITargetable
{
    // @formatter:off 
    public BuildingCard Model;
    public bool IsIndestructable;
    public HashSet<Building> AffectedBy = new HashSet<Building>();
    // @formatter:on 

    public void DoStartOfTurnEffects()
    {
        if (Model != null)
        {
            foreach (Effect effect in Model.EachTurnEffects)
            {
                effect.Activate();
            }
        }
    }

    public void OnConstruction()
    {
    }

    public void OnDeconstruction()
    {
    }

    public void DestroyBuilding()
    {
        Destroy(gameObject);
    }
}