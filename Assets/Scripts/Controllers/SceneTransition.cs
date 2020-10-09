using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public bool IsTimedTransition;
    public float DurationInSeconds = 3.0f;

    public string SceneToLoad;
    public Color32 CameraColor;
    public KeyCode SkipKey;

    private void Start()
    {
        Camera.main.backgroundColor = CameraColor;
        if (IsTimedTransition)
        {
            Invoke(nameof(GoToScene), DurationInSeconds);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(SkipKey))
        {
            CancelInvoke();
            GoToScene();
        }
    }

    private void GoToScene()
    {
        SceneManager.LoadScene(SceneToLoad);
    }
}