using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SortSpriteByY : MonoBehaviour
{
    private SpriteRenderer _renderer;
    private int _initialOffset;
    private Transform _root;

    private void Start()
    {
        _renderer      = GetComponent<SpriteRenderer>();
        _root          = transform.parent != null ? transform.parent : transform;
        _initialOffset = _renderer.sortingOrder;
    }

    private void Update()
    {
        _renderer.sortingOrder = -Mathf.RoundToInt(_root.position.y);
    }
}