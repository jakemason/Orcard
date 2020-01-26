using UnityEngine;

public class FollowsMouse : MonoBehaviour
{
    public void Update()
    {
        transform.position = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}