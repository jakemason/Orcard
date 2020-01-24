using UnityEngine;
using UnityEngine.EventSystems;

//TODO - Massive kludge for quick iteration testing. Will need a much more robust input system for prod.
public class DisablePlayerInputOnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        TowerPlacementController.Disabled = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TowerPlacementController.Disabled = false;
    }
}