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
    [Header("Enemy Info")]
    public List<Wave> Waves;
    [ReadOnly] public List<Enemy> EnemiesInCurrentWave;
    
    [Header("Wave Info")]
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
        if (CurrentWave > Waves.Count - 1)
        {
            SceneManager.LoadScene("Winner");
            return;
        }

        EnemiesInCurrentWave = Waves[CurrentWave].EnemiesInWave;
        Player.EndTurn();
        for (int i = 0; i < EnemiesInCurrentWave.Count; i++)
        {
            StartCoroutine(SpawnEnemy(EnemiesInCurrentWave[i], Waves[CurrentWave].TimeBetweenSpawns * i));
        }

        EnemiesRemainingInWave = EnemiesInCurrentWave.Count;
    }

    IEnumerator SpawnEnemy(Enemy toSpawn, float delay)
    {
        yield return new WaitForSeconds(delay);
        GameObject      newSpawn        = Instantiate(EnemyPrefab);
        EnemyController enemyController = newSpawn.GetComponent<EnemyController>();
        enemyController.Model = toSpawn;
        enemyController.MarkAlive();
        EnemiesSpawned.Add(newSpawn);
    }
}