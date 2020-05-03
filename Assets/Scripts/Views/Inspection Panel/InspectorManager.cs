using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class InspectorManager : MonoBehaviour
{
    public static InspectorManager Instance;

    // @formatter:off 
    [Header("Inspection Targets")]
    public GameObject InspectionPanel;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Details;
    
    [Header("Tower Inspection")]
    public GameObject TopPanel;
    public TextMeshProUGUI Ammo;
    public TextMeshProUGUI Damage;
    public TextMeshProUGUI AttackRate;
    // @formatter:on 

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public static void Enable()
    {
        Instance.InspectionPanel.SetActive(true);
    }

    public static void Disable()
    {
        Instance.InspectionPanel.SetActive(false);
    }
}