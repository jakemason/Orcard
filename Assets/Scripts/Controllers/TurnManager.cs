using Players;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public static void StartTurn()
    {
        Player.StartTurn();
        TowerManager.StartTurn();
        WaveController.Instance.NextWaveButton.SetActive(true);
    }
}