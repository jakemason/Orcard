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
    public float TimeBetweenWavesInSeconds = 120.0f;
     public float TimeBetweenWavesTracker;
    
    [Header("Enemy Info")]
    public List<Wave> Waves;
    [ReadOnly] public List<Enemy> EnemiesInCurrentWave;
    
    [Header("Wave Info")]
    public int CurrentWaveNumber = 0;
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
        RewardsManager.OpenRewardsPanel();
    }

    public void StartNextWave()
    {
        TimeBetweenWavesTracker = TimeBetweenWavesInSeconds;
        WaveActive              = true;
        NextWaveButton.SetActive(false);
        if (CurrentWaveNumber > Waves.Count - 1)
        {
            SceneManager.LoadScene("Winner");
            return;
        }

        EnemiesInCurrentWave = Waves[CurrentWaveNumber].EnemiesInWave;
        Player.EndTurn();
        for (int i = 0; i < EnemiesInCurrentWave.Count; i++)
        {
            StartCoroutine(SpawnEnemy(EnemiesInCurrentWave[i], Waves[CurrentWaveNumber].TimeBetweenSpawns * i));
        }

        EnemiesRemainingInWave = EnemiesInCurrentWave.Count;
    }

    public static Wave GetCurrentWave()
    {
        return Instance.Waves[Instance.CurrentWaveNumber];
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