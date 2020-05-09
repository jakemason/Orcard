using System.Collections;
using TMPro;
using UnityEngine;

public class TextBuilder : MonoBehaviour
{
    public float SecondsBetweenLetters = 0.5f;
    public TextMeshProUGUI TextBox;
    private static TextBuilder _instance;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }

        Clear();
    }

    public static void WriteText(string text)
    {
        for (int i = 0; i < text.Length; i++)
        {
            DoCoroutine(text, i);
        }
    }

    public static void Clear()
    {
        _instance.TextBox.text = "";
    }

    private static void DoCoroutine(string text, int index)
    {
        _instance.StartCoroutine(Write(text[index],
            _instance.SecondsBetweenLetters * (index + 1)));
    }

    private static IEnumerator Write(char toWrite, float delay)
    {
        yield return new WaitForSeconds(delay);
        _instance.TextBox.text += toWrite;
    }
}