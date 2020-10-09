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

    public static void Play(AudioClip clip, float pitchMin = 1.0f, float pitchMax = 1.0f, float volume = 1.0f)
    {
        // TODO:
        // A single Audio Source here isn't good enough because we can't control sound levels per Audio Type
        // Example: I want tower attacks to be 0.1f volume and the draw new hand effect to be 1.0f.
        // With this current setup it's possible for tower attacks to interfere with other sound effects if they
        // occur next to each other
        AudioSource source = Camera.main.GetComponent<AudioSource>();
        source.pitch  = Random.Range(pitchMin, pitchMax);
        source.volume = volume;
        source.PlayOneShot(clip);
        source.pitch = 1.0f;
    }
}