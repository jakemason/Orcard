using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Timer : MonoBehaviour
{
    public TextMeshProUGUI Text;
    private bool _isActive;
    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;
        string minutes = Mathf.Floor(timer / 60).ToString("00");
        string seconds = Mathf.Floor(timer % 60).ToString("00");
        Text.text = minutes + ":" + seconds;
    }
}