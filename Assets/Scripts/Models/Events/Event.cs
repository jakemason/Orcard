using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Events are in-game events that can grant boons or penalties.
/// </summary>
[CreateAssetMenu(fileName = "New Event", menuName = "Event")]
public class Event : ScriptableObject
{
    public string EventTitle;
    [TextArea] public string EventDescription;
    public Image EventImage;
    public List<Requirement> EventRequirements;
    public List<Effect> AcceptOutcomes;
    public List<Effect> DeclineOutcomes;
    [TextArea] public string DeclineText;
    [TextArea] public string AcceptText;

    public void Accept()
    {
        foreach (Effect positiveOutcome in AcceptOutcomes)
        {
            positiveOutcome.Activate();
        }
    }

    public void Decline()
    {
        foreach (Effect negativeOutcome in DeclineOutcomes)
        {
            negativeOutcome.Activate();
        }
    }

#if UNITY_EDITOR
    public void OnValidate()
    {
        string assetPath = AssetDatabase.GetAssetPath(GetInstanceID());
        AssetDatabase.RenameAsset(assetPath, EventTitle);
    }
#endif
}