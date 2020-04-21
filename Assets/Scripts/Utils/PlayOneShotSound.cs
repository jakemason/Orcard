using UnityEngine;

public class PlayOneShotSound : MonoBehaviour
{
    public AudioClip ToPlay;
    public float PitchMin = 0.7f;
    public float PitchMax = 1.2f;

    private void Start()
    {
        AudioSource source = Camera.main.GetComponent<AudioSource>();
        source.pitch = Random.Range(PitchMin, PitchMax);
        source.PlayOneShot(ToPlay);
        source.pitch = 1.0f;
    }
}