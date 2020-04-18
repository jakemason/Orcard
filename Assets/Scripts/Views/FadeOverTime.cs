using UnityEngine;
using UnityEngine.Serialization;

public class FadeOverTime : MonoBehaviour
{
    public float FadeSpeed = 0.5f;
    private SpriteRenderer _renderer;
    private Color _colorTracker;
    private bool _isActive = false;

    private void Start()
    {
        _renderer     = GetComponent<SpriteRenderer>();
        _colorTracker = _renderer.color;
    }

    public void Activate()
    {
        _isActive = true;
    }

    private void Update()
    {
        if (!_isActive) return;

        _colorTracker = new Color(_colorTracker.r, _colorTracker.g, _colorTracker.b,
            _colorTracker.a - (FadeSpeed * Time.deltaTime));
        _renderer.color = _colorTracker;

        if (_colorTracker.a <= 0.0f)
        {
            Destroy(this);
        }
    }
}