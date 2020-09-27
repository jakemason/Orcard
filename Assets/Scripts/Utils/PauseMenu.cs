using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseMenuPanel;
    private bool _gamePaused;

    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.Escape) || ShopView.IsEnabled()) return;

        if (_gamePaused)
        {
            Time.timeScale = 1.0f;
            _gamePaused    = false;
            PauseMenuPanel.SetActive(false);
        }
        else
        {
            Time.timeScale = 0.0f;
            PauseMenuPanel.SetActive(true);
            _gamePaused = true;
        }
    }

    public void AdjustSound(float value)
    {
        //TODO: Can avoid searching here...
        AudioSource[] audioSources = FindObjectsOfType<AudioSource>();
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.volume = value;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Restart()
    {
        Time.timeScale = 1.0f;
        _gamePaused    = false;
        SceneManager.LoadScene("BattleScreen");
    }
}