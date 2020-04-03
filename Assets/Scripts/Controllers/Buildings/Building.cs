using UnityEngine;

//[ExecuteInEditMode]
public class Building : MonoBehaviour, ITargetable
{
    // @formatter:off 
    public BuildingCard Model;
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
}