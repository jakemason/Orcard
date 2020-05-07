using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Narrator : MonoBehaviour
{
    public List<AudioClip> Alphabet;

    public float Pitch;
    public float PitchVariance = 0.2f;
    public float SecondsBetweenLetters = 0.2f;
    private static Narrator _instance;
    private AudioSource _source;

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

        if (Alphabet.Count != 27)
        {
            Debug.LogWarning("Narrator does not have 27 letters to pronounce!");
        }

        _source = gameObject.AddComponent<AudioSource>();
        ReadText("Hello friend, welcome to my gambit. Please wait a while to be seated.");
    }

    public void ReadText(string text)
    {
        _source.pitch = Pitch + Random.Range(-PitchVariance, PitchVariance);
        text          = text.ToLower();
        for (int i = 0; i < text.Length; i += 2)
        {
            int val = text[i] - 97;

            if (val > 27 || val < 0) continue;
            StartCoroutine(Speak(val, SecondsBetweenLetters * (i + 1)));
        }
    }

    IEnumerator Speak(int toSpeak, float delay)
    {
        yield return new WaitForSeconds(delay);
        Debug.Log("Speaking");
        _source.PlayOneShot(Alphabet[toSpeak]);
    }
}