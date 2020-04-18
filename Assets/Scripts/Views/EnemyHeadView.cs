using UnityEngine;

public class EnemyHeadView : MonoBehaviour
{
    private SpriteRenderer _renderer;
    private bool _isActive;
    public float HurtDuration = 1f;
    private float _duration;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (!_isActive) return;

        _duration -= Time.deltaTime;
        if (_duration <= 0f)
        {
            _isActive         = false;
            _renderer.enabled = false;
        }
    }

    public void Activate()
    {
        _duration         = HurtDuration;
        _isActive         = true;
        _renderer.enabled = true;
    }
}