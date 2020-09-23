using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ImageFade : MonoBehaviour
{
    public float FadeSpeed = 1.0f;
    public float Delay = 0.0f;
    private Image _image;

    private void Start()
    {
        _image = GetComponent<Image>();
        Color color = _image.color;
        color.a      = 1;
        _image.color = color;
    }

    private void Update()
    {
        Delay -= Time.deltaTime;
        if (Delay <= 0.0f)
        {
            Color color = _image.color;
            color = new Color(color.r, color.g, color.b,
                color.a - FadeSpeed * Time.deltaTime);
            _image.color = color;
            if (color.a <= 0.0f)
            {
                Destroy(gameObject);
            }
        }
    }
}