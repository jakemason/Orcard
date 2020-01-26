using System.Collections.Generic;
using Players;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveController : MonoBehaviour
{
    public static WaveController Instance;
    public List<Enemy> EnemiesToPullFrom;
    public int EnemiesToSpawn = 5;
    public int AdditionalEnemiesPerWave = 2;
    public float TimeBetweenIndividualSpawns = 0.5f;
    public int CurrentWave = 0;
    public List<GameObject> EnemiesSpawned;
    public GameObject EnemyPrefab;
    public GameObject NextWaveButton;
    public bool WaveActive = false;

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
        //TODO: Can't do this, instead we should decrement a counter every time an enemy is
        //TODO: killed and once the counter is 0 we know we're in the clear
        if (WaveActive)
        {
            CheckForClear();
        }
    }

    private void CheckForClear()
    {
        if (EnemiesSpawned.Count == 0)
        {
            TowerManager.StartTurn();
            NextWaveButton.SetActive(true);
            CurrentWave++;
            WaveActive = false;
            RewardsManager.OpenRewardsPanel();
        }
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