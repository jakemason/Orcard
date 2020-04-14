using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class TowerInspector : MonoBehaviour, IInspectable, IPointerEnterHandler, IPointerExitHandler
{
    [FormerlySerializedAs("BuildingReference")]
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
        InspectorManager.Instance.Name.text = "<b>" + Model.Name + "</b>";

        string details = "";
        //only show damage values if we actually fire a bolt of some sort
        //this isn't super robust, may need a better means of checking this in the future.
        if (Model.BoltPrefab != null)
        {
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

            details += "<b>Ammo:</b> " + TowerReference.RemainingAmmo + " / " + TowerReference.MaxAmmo + "\n";
            ;
        }

        if (Model.IsIndestructible)
        {
            details += "<b>Indestructible.</b>" + "\n";
        }

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