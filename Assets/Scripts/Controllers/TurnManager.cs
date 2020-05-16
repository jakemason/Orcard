using System.Collections.Generic;
using Players;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static List<TimedEffect> TimedEffects = new List<TimedEffect>();

    public void Update()
    {
        for (int i = TimedEffects.Count - 1; i >= 0; i--)
        {
            TimedEffects[i].DelayCoolddown -= Time.deltaTime;
            if (TimedEffects[i].DelayCoolddown <= 0.0f)
            {
                foreach (Effect effect in TimedEffects[i].Effects)
                {
                    effect.Activate();
                }

                if (!TimedEffects[i].IsPermanent)
                {
                    TimedEffects.RemoveAt(i);
                }
            }
        }
    }

    public static void StartTurn()
    {
        PlayerController.StartTurn();
        BuildingManager.StartTurn();
        //TODO: Conditional checks for Events and Shop goes here most likely. WaveControllerButton conditional on those being completed
        //TODO: Do IncomeController from here as well?
    }
}