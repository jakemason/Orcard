using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Core : MonoBehaviour
{
    public int MaxHealth = 50;
    private int _health;
    public TextMeshProUGUI HealthText;

    public int Health
    {
        get { return _health; }
        set
        {
            _health = value;
            if (_health > MaxHealth)
            {
                _health = MaxHealth;
            }

            UpdateText();
        }
    }

    private void Start()
    {
        Health             = MaxHealth;
        transform.position = MapController.Instance.Waypoints[0].position;
    }

    public void UpdateText()
    {
        HealthText.text = "Health: " + _health + " / " + MaxHealth;
    }

    public void TakeDamage(int damageToTake)
    {
        Health -= damageToTake;
        if (Health > 0) return;

        Debug.Log("So long, sailor man.");
        SceneManager.LoadScene("You Died");
    }
}