using System;
using TMPro;
using UnityEngine;

public class ClosedCaptioning : MonoBehaviour
{
    private static ClosedCaptioning _instance;
    public GameObject FloatingTextPrefab;
    public Transform SpawnPoint;
    public static bool IsEnabled = true;

    private void Start()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public static GameObject GetFloatingTextPrefab()
    {
        return _instance.FloatingTextPrefab;
    }

    public static Transform GetSpawnPoint()
    {
        return _instance.SpawnPoint;
    }

    public static void CreateMessage(string message)
    {
        if (!IsEnabled) return;

        Transform spawnPoint = GetSpawnPoint();
        GameObject textObject =
            Instantiate(GetFloatingTextPrefab(), spawnPoint.position,
                Quaternion.identity,             spawnPoint);
        textObject.name = "Floating Text";
        TextMeshProUGUI text = textObject.GetComponent<TextMeshProUGUI>();
        text.text = $"[ {message} ]";
    }
}