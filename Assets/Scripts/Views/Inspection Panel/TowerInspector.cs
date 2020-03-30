using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerInspector : MonoBehaviour, IInspectable, IPointerEnterHandler, IPointerExitHandler
{
    public Tower TowerReference;
    public TowerCard Model;

    private void Start()
    {
        if (TowerReference != null)
        {
            Model = TowerReference.Model;
        }
    }

    public void EnableInspection()
    {
        InspectorManager.Instance.Sprite.sprite = Model.Artwork;
        InspectorManager.Instance.Name.text     = "<b>" + Model.Name + "</b>";

        string details = "";
        if (Model.Damage != 0)
        {
            details += "<b>Damage</b>: " + Model.Damage + "\n";
        }

        if (Model.AttackRate != 0.0f)
        {
            details += "<b>Attack Rate</b>: " + Model.AttackRate.ToString("n2") + "\n";
        }

        if (Model.DamagePerSecond != 0.0f)
        {
            details += "<b>DPS</b>: " + Model.DamagePerSecond + "\n";
        }

        details                                += Model.InstructionText;
        InspectorManager.Instance.Details.text =  details;
        InspectorManager.Enable();
    }

    public void OnMouseOver()
    {
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