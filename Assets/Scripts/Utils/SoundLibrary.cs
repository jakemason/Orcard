using UnityEngine;
using UnityEngine.Serialization;

public class SoundLibrary : MonoBehaviour
{
    public static SoundLibrary Global;
    [FormerlySerializedAs("ErrorSound")] public AudioClip Error;
    public AudioClip ButtonClick;

    private void Start()
    {
        if (Global == null)
        {
            Global = this;
        }
        else
        {
            Destroy(this);
        }
    }
}