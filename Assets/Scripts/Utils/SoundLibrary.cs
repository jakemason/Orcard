using UnityEngine;
using UnityEngine.Serialization;

public class SoundLibrary : MonoBehaviour
{
    public static SoundLibrary Global;
    public AudioClip Error;
    public AudioClip ButtonHover;
    public AudioClip Click;

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