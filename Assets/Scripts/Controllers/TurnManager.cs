using System.Collections.Generic;
using Players;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public int TimeBetweenTurnsInSeconds = 5;
    public static List<TimedEffect> TimedEffects;
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

        for (int i = TimedEffects.Count - 1; i >= 0; i--)
        {
            TimedEffects[i].DelayCoolddown -= Time.deltaTime;
            if (TimedEffects[i].DelayCoolddown <= 0.0f)
            {
                foreach (Effect effect in TimedEffects[i].Effects)
                {
                    effect.Activate();
                }

                TimedEffects.RemoveAt(i);
            }
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