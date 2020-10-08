using UnityEngine;

public class SoundLibrary : MonoBehaviour
{
    public static SoundLibrary Global;
    public AudioClip ErrorSound;

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