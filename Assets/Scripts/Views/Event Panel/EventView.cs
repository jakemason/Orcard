using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventView : MonoBehaviour
{
    public static EventView Instance;

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

    public TextMeshProUGUI Description;
    public Image EventImage;
    public TextMeshProUGUI AcceptText;
    public TextMeshProUGUI DeclineText;

    public GameObject EventPanelRoot;

    public static void Close()
    {
        Instance.EventPanelRoot.SetActive(false);
    }

    public static void Open(Event toPresent)
    {
        Instance.EventPanelRoot.SetActive(true);
        Instance.Description.text = toPresent.EventDescription;
        Instance.EventImage       = toPresent.EventImage;
        Instance.AcceptText.text  = toPresent.AcceptText;
        Instance.DeclineText.text = toPresent.DeclineText;
    }
}