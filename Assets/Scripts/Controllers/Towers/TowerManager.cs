using Boo.Lang;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public static TowerManager Instance;
    public List<Tower> ConstructedTowers;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        ConstructedTowers = new List<Tower>();
    }

    public static void StartTurn()
    {
        foreach (Tower tower in Instance.ConstructedTowers)
        {
            tower.DoStartOfTurnEffects();
        }
    }
}