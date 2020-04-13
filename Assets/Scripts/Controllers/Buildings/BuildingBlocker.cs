using UnityEngine;

public class BuildingBlocker : MonoBehaviour
{
    public Sprite Sprite;

    private void Start()
    {
        Vector3 position = transform.position;
        int     ypos     = (int) position.y;
        int     xpos     = (int) position.x;
        Debug.Log(position);
        Debug.Log("Blocking:" + new Vector2(xpos,                       ypos));
        BuildingManager.Instance.ConstructedBuildings[new Vector2(xpos, ypos)] = gameObject.AddComponent<Building>();

        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
        renderer.sprite = Sprite;
    }
}