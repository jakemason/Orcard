using UnityEngine;

public class EventController : MonoBehaviour
{
    public Event CurrentEvent;

    public void StartEvent()
    {
        EventView.Open(CurrentEvent);
    }

    public void AcceptEvent()
    {
        CurrentEvent.Accept();
        EventView.Close();
    }

    public void DeclineEvent()
    {
        CurrentEvent.Decline();
        EventView.Close();
    }
}