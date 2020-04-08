using Players;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static void StartTurn()
    {
        Player.StartTurn();
        BuildingManager.StartTurn();
        //TODO: Conditional checks for Events and Shop goes here most likely. WaveControllerButton conditional on those being completed
        WaveController.Instance.NextWaveButton.SetActive(true);
    }
}