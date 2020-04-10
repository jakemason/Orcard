using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Core : MonoBehaviour
{
    public static Core Instance;
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

            if (_health < 0)
            {
                SceneManager.LoadScene("You Died");
            }

            UpdateText();
        }
    }

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

        Health             = MaxHealth;
        transform.position = MapController.Instance.Waypoints[0].position;
    }

    public void UpdateText()
    {
        HealthText.text = _health.ToString();
    }

    public static void TakeDamage(int damageToTake)
    {
        Instance.Health -= damageToTake;
        if (Instance.Health > 0) return;

        Debug.Log("So long, sailor man.");
        SceneManager.LoadScene("You Died");
    }
}