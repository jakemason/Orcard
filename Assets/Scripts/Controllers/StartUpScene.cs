using UnityEngine;
using UnityEngine.SceneManagement;

public class StartUpScene : MonoBehaviour
{
    public float DurationInSeconds = 3.0f;

    private void Start()
    {
        Invoke(nameof(GoToGame), DurationInSeconds);
    }

    private void GoToGame()
    {
        SceneManager.LoadScene("BattleScreen");
    }
}