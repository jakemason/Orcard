using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class BasicInspector : MonoBehaviour, IInspectable, IPointerEnterHandler, IPointerExitHandler
{
    [FormerlySerializedAs("TowerReference")]
    public Building BuildingReference;

    public Card Model;

    private void Start()
    {
        if (BuildingReference != null)
        {
            Model = BuildingReference.Model;
        }
    }

    public void EnableInspection()
    {
        InspectorManager.Instance.Name.text = "<b>" + Model.Name + "</b>";

        string details = "";
        details                                += Model.InstructionText;
        details                                += "\n<i>" + Model.FlavorText + "</i>";
        InspectorManager.Instance.Details.text =  details;
        InspectorManager.Enable();
    }

    public void OnMouseOver()
    {
        if (RewardsManager.IsOpen) return;
        EnableInspection();
    }

    public void OnMouseExit()
    {
        InspectorManager.Disable();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        EnableInspection();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        InspectorManager.Disable();
    }
}