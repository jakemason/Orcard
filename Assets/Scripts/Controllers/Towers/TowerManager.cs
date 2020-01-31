using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public static TowerManager Instance;
    public Dictionary<Vector2, Tower> ConstructedTowers;

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

        ConstructedTowers = new Dictionary<Vector2, Tower>();
    }

    public static void StartTurn()
    {
        foreach (KeyValuePair<Vector2, Tower> tower in Instance.ConstructedTowers)
        {
            tower.Value.DoStartOfTurnEffects();
        }
    }
}