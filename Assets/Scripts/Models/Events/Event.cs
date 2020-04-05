using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Events are in-game events that can grant boons or penalties.
/// </summary>
[CreateAssetMenu(fileName = "New Event", menuName = "Event")]
public class Event : ScriptableObject
{
    public List<Effect> PositiveOutcomes;
    public string AcceptText;

    public List<Effect> NegativeOutcomes;
    public string DeclineText;

    public Image EventImage;
    public string EventDescription;

    public void Accept()
    {
        foreach (Effect positiveOutcome in PositiveOutcomes)
        {
            positiveOutcome.Activate();
        }
    }

    public void Decline()
    {
        foreach (Effect negativeOutcome in NegativeOutcomes)
        {
            negativeOutcome.Activate();
        }
    }
}