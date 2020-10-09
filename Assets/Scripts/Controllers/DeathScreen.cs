using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public void Restart()
    {
        PlayOneShotSound.Play(SoundLibrary.Global.ButtonClick, 0.9f, 1.0f);
        SceneManager.LoadScene("BattleScreen");
    }

    public void QuitGame()
    {
        PlayOneShotSound.Play(SoundLibrary.Global.ButtonClick, 0.9f, 1.0f);
        Application.Quit();
    }
}