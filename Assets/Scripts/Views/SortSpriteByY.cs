using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SortSpriteByY : MonoBehaviour
{
    private SpriteRenderer _renderer;
    private int _initialOffset;
    private Transform _root;

    /*
     * This is very important. It allows us to do our usual SortByY, but also allows us to tweak things within this
     * area for sorting as well. In other words, this allows us some wiggle room to tweak sorting orders even across
     * Y values. The Y value will _always_ dominate, but now we can use  _initialOffset to tweak values along the same
     * Y where we previously had conflicts such as the Orc Warrior accessories.
     */
    private int _yValueWeight = 100;

    private void Start()
    {
        _renderer      = GetComponent<SpriteRenderer>();
        _root          = transform.parent != null ? transform.parent : transform;
        _initialOffset = _renderer.sortingOrder;
    }

    private void Update()
    {
        _renderer.sortingOrder = (-Mathf.RoundToInt(_root.position.y) * _yValueWeight) + _initialOffset;
    }
}