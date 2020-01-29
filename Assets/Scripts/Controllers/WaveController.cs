using System.Collections.Generic;
using Players;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveController : MonoBehaviour
{
    // @formatter:off 
    public static WaveController Instance;
    [Header("Enemy Info")]
    public List<Enemy> EnemiesToPullFrom;
    public int EnemiesToSpawn = 5;
    
    [Header("Wave Info")]
    public float TimeBetweenIndividualSpawns = 0.5f;
    public int CurrentWave = 0;
    public bool WaveActive = false;
    public int EnemiesRemainingInWave;
    public int AdditionalEnemiesPerWave = 2;
    public List<GameObject> EnemiesSpawned;
    
    [Header("GameObject References")]
    public GameObject EnemyPrefab;
    public GameObject NextWaveButton;
    // @formatter:on 
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

        EnemiesSpawned = new List<GameObject>();
    }

    private void LateUpdate()
    {
        if (WaveActive && EnemiesRemainingInWave == 0)
        {
            EndEncounter();
        }
    }

    private void EndEncounter()
    {
        TowerManager.StartTurn();
        NextWaveButton.SetActive(true);
        CurrentWave++;
        WaveActive = false;
        RewardsManager.OpenRewardsPanel();
    }

    public void SpawnEnemies()
    {
        WaveActive = true;
        NextWaveButton.SetActive(false);
        Player.EndTurn();
        for (int i = 0; i < EnemiesToSpawn + (CurrentWave * AdditionalEnemiesPerWave); i++)
        {
            Invoke("SpawnRandomEnemy", TimeBetweenIndividualSpawns * i);
        }

        EnemiesRemainingInWave = EnemiesToSpawn + (CurrentWave * AdditionalEnemiesPerWave);
    }

    private void SpawnRandomEnemy()
    {
        int             enemyToSpawnIndex = Random.Range(0, EnemiesToPullFrom.Count);
        GameObject      newSpawn          = Instantiate(EnemyPrefab);
        EnemyController enemyController   = newSpawn.GetComponent<EnemyController>();
        enemyController.Model = EnemiesToPullFrom[enemyToSpawnIndex];
        enemyController.MarkAlive();
        EnemiesSpawned.Add(newSpawn);
    }
}