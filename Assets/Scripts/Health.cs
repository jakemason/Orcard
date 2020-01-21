using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int MaxHealth;
    public int CurrentHealth;
    public bool MarkedForDeath;
    public TextMeshProUGUI HP;

    public void SetStartingHealth(int health)
    {
        MaxHealth     = health;
        CurrentHealth = health;
        UpdateHealthText();
    }

    public void Heal(int amountToHeal)
    {
        CurrentHealth += amountToHeal;
        if (CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }

        UpdateHealthText();
    }

    public void TakeDamage(int damageToTake)
    {
        CurrentHealth -= damageToTake;
        UpdateHealthText();

        if (CurrentHealth <= 0)
        {
            MarkedForDeath = true;
        }
    }

    private void UpdateHealthText()
    {
        if (HP)
        {
            HP.text = CurrentHealth.ToString();
        }
    }
}