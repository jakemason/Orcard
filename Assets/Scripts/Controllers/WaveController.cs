using System;
using System.Collections;
using System.Collections.Generic;
using Players;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class WaveController : MonoBehaviour
{
    // @formatter:off 
    public static WaveController Instance;
    [Header("Wave Timings")]
    public float TimeBetweenWavesInSeconds = 60.0f;
    public float TimeBetweenWavesTracker;
    public float TimeBetweenSpawns = 1.0f;
    public AnimationCurve DifficultyCurve;
     
    [Header("Enemy Info")]
    public Enemy EnemyToSpawn;
   // [ReadOnly] public List<Enemy> EnemiesInCurrentWave;
    
    [Header("Wave Info")]
    public int CurrentWaveNumber = 1;
    public bool WaveActive = false;
    public int EnemiesRemainingInWave;
    public int AdditionalEnemiesPerWave = 2;
    public List<GameObject> EnemiesSpawned;
    
    [Header("GameObject References")]
    public GameObject EnemyPrefab;
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

        TimeBetweenWavesTracker = TimeBetweenWavesInSeconds;
        EnemiesSpawned          = new List<GameObject>();
    }

    private void Update()
    {
        if (WaveActive) return;
        TimeBetweenWavesTracker -= Time.deltaTime;
        if (TimeBetweenWavesTracker <= 0.0f)
        {
            StartNextWave();
        }
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
        CurrentWaveNumber++;
        WaveActive = false;
        RewardsManager.MakeNewRewardAvailable();
        //RewardsManager.OpenRewardsPanel();
    }

    public void StartNextWave()
    {
        TimeBetweenWavesTracker = TimeBetweenWavesInSeconds;
        WaveActive              = true;
        if (CurrentWaveNumber > 50)
        {
            SceneManager.LoadScene("Winner");
            return;
        }

        int toSpawn = CurrentWaveNumber;

        for (int i = 0; i < toSpawn; i++)
        {
            StartCoroutine(SpawnEnemy(EnemyToSpawn, TimeBetweenSpawns * i));
        }

        EnemiesRemainingInWave = toSpawn;
    }

    private IEnumerator SpawnEnemy(Enemy toSpawn, float delay)
    {
        yield return new WaitForSeconds(delay);
        GameObject      newSpawn        = Instantiate(EnemyPrefab);
        EnemyController enemyController = newSpawn.GetComponent<EnemyController>();
        enemyController.Model    =  Instantiate(toSpawn);
        enemyController.Model.HP += CurrentWaveNumber;
        enemyController.MarkAlive();
        EnemiesSpawned.Add(newSpawn);
    }
}