using UnityEngine;

public class MoveTo : MonoBehaviour
{
    private RectTransform _transform;
    public int XOffset;
    public int YOffset;
    public float Speed;

    private Vector2 _startingPos;
    private Vector2 _endPos;
    public ParallaxMouseFollow ScriptToEnable;

    private void Start()
    {
        _transform   = GetComponent<RectTransform>();
        _startingPos = _transform.anchoredPosition;
        _endPos      = _startingPos + new Vector2(XOffset, YOffset);
    }

    private void Update()
    {
        if (Vector2.Distance(_transform.anchoredPosition, _endPos) >= 0.5f)
        {
            _transform.anchoredPosition =
                Vector2.MoveTowards(_transform.anchoredPosition, _endPos, Speed * Time.deltaTime);
        }
        else
        {
            ScriptToEnable.enabled = true;
        }
    }
}