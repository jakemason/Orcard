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
    public Button AcceptButton;
    public TextMeshProUGUI AcceptText;
    public TextMeshProUGUI DeclineText;
    public GameObject EventPanelRoot;

    public static void Close()
    {
        Instance.EventPanelRoot.SetActive(false);
    }

    public static void Open(Event toPresent)
    {
        Instance.AcceptButton.interactable = true;
        Instance.EventPanelRoot.SetActive(true);
        Instance.Description.text = toPresent.EventDescription;
        Instance.EventImage       = toPresent.EventImage;
        Instance.AcceptText.text  = toPresent.AcceptText;
        bool acceptRequirementsMet = true;

        foreach (Requirement requirement in toPresent.EventRequirements)
        {
            acceptRequirementsMet = acceptRequirementsMet && requirement.RequirementMet();
        }

        if (!acceptRequirementsMet)
        {
            Instance.AcceptButton.interactable = false;
        }

        Instance.DeclineText.text = toPresent.DeclineText;
    }
}