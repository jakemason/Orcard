using UnityEngine;

public class BuildingBlocker : MonoBehaviour
{
    public Sprite Sprite;
    public bool IsIndestructable;

    private void Start()
    {
        Vector3  position = transform.position;
        int      ypos     = (int) position.y;
        int      xpos     = (int) position.x;
        Building b        = gameObject.AddComponent<Building>();
        b.IsIndestructable                                                     = IsIndestructable;
        BuildingManager.Instance.ConstructedBuildings[new Vector2(xpos, ypos)] = b;

        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
        renderer.sprite = Sprite;
        if (Sprite != null)
        {
            renderer.color = Color.white;
        }
    }
}