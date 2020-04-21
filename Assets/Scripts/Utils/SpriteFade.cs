using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SpriteFade : MonoBehaviour
{
    public float FadeSpeed = 1.0f;
    private SpriteRenderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Color color = _renderer.color;
        color = new Color(color.r, color.g, color.b,
            color.a - FadeSpeed * Time.deltaTime);
        _renderer.color = color;
    }
}