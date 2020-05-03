using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wave", menuName = "Wave")]
public class Wave : ScriptableObject
{
    public List<Enemy> EnemiesInWave;
    public float TimeBetweenSpawns = 0.5f;
    public bool RewardsSpecificRarity = false;
    public int EstimatedDifficulty;
    public Card.CardRarity ToReward = Card.CardRarity.Common;

#if UNITY_EDITOR
    private void OnValidate()
    {
        EstimatedDifficulty = 0;
        foreach (Enemy enemy in EnemiesInWave)
        {
            EstimatedDifficulty += (int) enemy.OverallStrength;
        }
    }
#endif
}