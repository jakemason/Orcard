using System;
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
    private Event _currentEvent;

    public static void Close()
    {
        Instance.EventPanelRoot.SetActive(false);
    }

    public void Update()
    {
        RequirementsAreMetCheck();
    }

    private static void RequirementsAreMetCheck()
    {
        if (Instance._currentEvent == null) return;

        bool acceptRequirementsMet = true;
        foreach (Requirement requirement in Instance._currentEvent.EventRequirements)
        {
            acceptRequirementsMet = acceptRequirementsMet && requirement.RequirementMet();
        }

        Instance.AcceptButton.interactable = acceptRequirementsMet;
    }

    public static void Open(Event toPresent)
    {
        Instance._currentEvent             = toPresent;
        Instance.AcceptButton.interactable = true;
        Instance.EventPanelRoot.SetActive(true);
        Instance.Description.text = toPresent.EventDescription;
        Instance.EventImage       = toPresent.EventImage;
        Instance.AcceptText.text  = toPresent.AcceptText;
        RequirementsAreMetCheck();
        Instance.DeclineText.text = toPresent.DeclineText;
    }
}