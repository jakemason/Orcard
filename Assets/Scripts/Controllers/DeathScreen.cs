﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScreen : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene("BattleScreen");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}