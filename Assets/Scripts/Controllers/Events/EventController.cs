using UnityEngine;
using UnityEngine.UI;

public class EventController : MonoBehaviour
{
    public Event CurrentEvent;
    public Button Button;
    public int TimeBetweenEventsInSeconds = 45;
    private float _eventTimer;

    private void Start()
    {
        _eventTimer = TimeBetweenEventsInSeconds;
    }

    private void Update()
    {
        _eventTimer -= Time.deltaTime;
        if (_eventTimer <= 0f)
        {
            MakeEventAvailable();
            _eventTimer = TimeBetweenEventsInSeconds;
        }
    }

    public void MakeEventAvailable()
    {
        Button.interactable = true;
    }

    public void StartEvent()
    {
        EventView.Open(CurrentEvent);
    }

    public void AcceptEvent()
    {
        CurrentEvent.Accept();
        EventView.Close();
        Button.interactable = false;
    }

    public void DeclineEvent()
    {
        CurrentEvent.Decline();
        EventView.Close();
        Button.interactable = false;
    }
}