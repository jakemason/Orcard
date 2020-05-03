using System;
using TMPro;
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
        //only show damage values if we actually fire a bolt of some sort
        //this isn't super robust, may need a better means of checking this in the future.
        if (Model.BoltPrefab != null)
        {
            InspectorManager.Instance.Damage.text     = Model.Damage.ToString();
            InspectorManager.Instance.AttackRate.text = Model.AttackRate.ToString("n2");
            if (TowerReference != null)
            {
                InspectorManager.Instance.Ammo.text = TowerReference.RemainingAmmo + " / " + TowerReference.MaxAmmo;
            }
            else
            {
                InspectorManager.Instance.Ammo.text = Model.MaxAmmo + " / " + Model.MaxAmmo;
            }
        }

        InspectorManager.Instance.TopPanel.SetActive(true);
    }

    public void OnMouseOver()
    {
        if (RewardsManager.IsOpen) return;

        EnableInspection();
    }

    public void OnMouseExit()
    {
        InspectorManager.Instance.TopPanel.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        EnableInspection();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        InspectorManager.Instance.TopPanel.SetActive(false);
    }
}