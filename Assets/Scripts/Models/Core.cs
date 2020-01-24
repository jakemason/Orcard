using UnityEngine;
using UnityEngine.SceneManagement;

public class Core : MonoBehaviour
{
    public int MaxHealth = 50;
    public int Health = 50;

    private void Start()
    {
        Health             = MaxHealth;
        transform.position = MapController.Instance.Waypoints[0].position;
    }

    public void TakeDamage(int damageToTake)
    {
        Health -= damageToTake;
        if (Health > 0) return;

        Debug.Log("So long, sailor man.");
        SceneManager.LoadScene("You Died");
    }
}