using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Narrator : MonoBehaviour
{
    public List<AudioClip> Alphabet;

    /**
     * Just minor variations on this pitch is pretty sweet.
     *
     * 0.4 sounds like some sort of shadow demon-y thing
     * 0.9 sounds like an orc or old guy
     * 1.2 sounds about normal
     * 1.4+ sounds like a cartoon-y, friendly sort of thing
     */
    public float Pitch;

    public int ReadEveryNthLetter = 1;

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

        _source = gameObject.GetComponent<AudioSource>();
        string test =
            "Sire, the orcish army is almost here and there's an entire horde of them! We must hold them back or the kingdom is doomed!";
        Say(test);
        TextBuilder.WriteText(test);
    }

    public void Say(string text)
    {
        _source.pitch = Pitch + Random.Range(-PitchVariance, PitchVariance);
        text          = text.ToLower();
        for (int i = 0; i < text.Length; i += ReadEveryNthLetter)
        {
            int val = text[i] - 97;

            if (val > 27 || val < 0) continue;
            //TODO: Can I just play these back-to-back rather than this weird delay? It would sound better if so...
            //TODO: Sometimes we get audio overlap if ReadEveryNthLetter == 1 with this current system
            StartCoroutine(Speak(val, SecondsBetweenLetters * (i + 1)));
        }
    }

    private IEnumerator Speak(int toSpeak, float delay)
    {
        yield return new WaitForSeconds(delay);
        _source.PlayOneShot(Alphabet[toSpeak]);
    }
}