using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    public float Speed = 12f;
    public Vector2 Bounds;

    private void Update()
    {
        float horizontalMovement = Input.GetAxis("Horizontal");
        if (horizontalMovement != 0.0f)
        {
            Vector3 pos = transform.position;
            pos.x              += horizontalMovement * Speed * Time.deltaTime;
            pos.x              =  Mathf.Clamp(pos.x, Bounds.x, Bounds.y);
            transform.position =  pos;
        }
    }
}