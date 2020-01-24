using System.Collections.Generic;
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
        if (WaveActive)
        {
            CheckForClear();
        }
    }

    private void CheckForClear()
    {
        if (EnemiesSpawned.Count == 0)
        {
            NextWaveButton.SetActive(true);
            PlayerResources.NextTurn();
            CurrentWave++;
            WaveActive                        = false;
            TowerPlacementController.Disabled = false;
        }
    }


    public void SpawnEnemies()
    {
        WaveActive = true;
        NextWaveButton.SetActive(false);
        PlayerResources.ModifyPlayerEnergy(-99);
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