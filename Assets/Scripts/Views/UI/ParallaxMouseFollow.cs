using System;
using UnityEngine;

public class ParallaxMouseFollow : MonoBehaviour
{
    public float VerticalSensitivity = 0.01f;
    public float HorizontalSensitivity = 0.01f;

    private Transform _transform;
    private Vector2 _startPos;
    private static Vector2 _screenMid = new Vector2(Screen.width / 2, Screen.height / 2);

    private void Start()
    {
        _transform = GetComponent<Transform>();
        _startPos  = _transform.position;
    }

    private void LateUpdate()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.x = Mathf.Clamp(mousePos.x, 0, Screen.width);
        mousePos.y = Mathf.Clamp(mousePos.y, 0, Screen.height);


        Vector3 transformPosition = _transform.position;
        transformPosition.x = _startPos.x + ((mousePos.x - _screenMid.x) * HorizontalSensitivity);
        transformPosition.y = _startPos.y + ((mousePos.y - _screenMid.y) * VerticalSensitivity);
        _transform.position = transformPosition;
    }
}