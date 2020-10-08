using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float ScrollSpeed;
    public float FadeTime;

    private float _startFadeTime;
    private TextMeshProUGUI _text;
    private RectTransform _rect;

    private void Awake()
    {
        _text          = GetComponent<TextMeshProUGUI>();
        _rect          = GetComponent<RectTransform>();
        _startFadeTime = FadeTime;
    }

    public void Update()
    {
        _rect.anchoredPosition += Vector2.up * (Time.deltaTime * ScrollSpeed);
        FadeTime               -= Time.deltaTime;
        Color color = _text.color;
        color.a     = FadeTime / _startFadeTime;
        _text.color = color;
        if (FadeTime <= 0.0f)
        {
            Destroy(gameObject);
        }
    }
}