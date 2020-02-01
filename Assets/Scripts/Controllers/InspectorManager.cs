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
    public Image Sprite;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Details;
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