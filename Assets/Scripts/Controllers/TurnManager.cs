using Players;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static void StartTurn()
    {
        Player.StartTurn();
        BuildingManager.StartTurn();
        WaveController.Instance.NextWaveButton.SetActive(true);
    }
}