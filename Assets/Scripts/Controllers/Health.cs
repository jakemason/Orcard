using UnityEditor;
using UnityEngine;

//TODO: Collapse this into EnemyController imo
public class Health : MonoBehaviour
{
    public int MaxHealth;
    public int CurrentHealth;
    public bool MarkedForDeath;
    public GameObject DeathPrefab;

    private void Start()
    {
        SetStartingHealth(MaxHealth);
    }

    public void SetStartingHealth(int health)
    {
        MaxHealth     = health;
        CurrentHealth = health;
    }

    public void Heal(int amountToHeal)
    {
        CurrentHealth += amountToHeal;
        if (CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
    }

    public void TakeDamage(int damageToTake)
    {
        CurrentHealth -= damageToTake;

        if (CurrentHealth <= 0 && !MarkedForDeath)
        {
            MarkedForDeath = true;
            //TODO: Do a better job with this
            WaveController.Instance.EnemiesSpawned.Remove(gameObject);
            WaveController.Instance.EnemiesRemainingInWave -= 1;
            GameObject deathObject =
                Instantiate(DeathPrefab, transform.position, Quaternion.identity);
            EnemyController      enemyController      = GetComponent<EnemyController>();
            EnemyDeathController enemyDeathController = deathObject.GetComponent<EnemyDeathController>();
            enemyDeathController.Activate(enemyController.Model);
            Destroy(gameObject);
        }
    }
}