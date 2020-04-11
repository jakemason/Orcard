using Players;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public int TimeBetweenTurnsInSeconds = 5;
    private float _timeCountdown;

    public void Start()
    {
        _timeCountdown = TimeBetweenTurnsInSeconds;
    }

    public void Update()
    {
        _timeCountdown -= Time.deltaTime;
        if (_timeCountdown <= 0f)
        {
            StartTurn();
            _timeCountdown = TimeBetweenTurnsInSeconds;
        }
    }

    public static void StartTurn()
    {
        Player.StartTurn();
        BuildingManager.StartTurn();
        //TODO: Conditional checks for Events and Shop goes here most likely. WaveControllerButton conditional on those being completed
        //TODO: Do IncomeController from here as well?
    }
}