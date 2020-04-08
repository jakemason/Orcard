using TMPro;
using UnityEngine;


//TODO: Honestly not even sure this is a good idea as a mechanic?
[RequireComponent(typeof(TextMeshProUGUI))]
public class WaveTimer : MonoBehaviour
{
    public TextMeshProUGUI Text;
    public GameObject UIElement;
    private bool _isActive;

    private void Update()
    {
        //TODO: Don't need to do this in Update()
        _isActive = !WaveController.Instance.WaveActive;
        UIElement.SetActive(_isActive);

        Text.text = "Time until next wave: " + (int) WaveController.Instance.TimeBetweenWavesTracker;
    }
}