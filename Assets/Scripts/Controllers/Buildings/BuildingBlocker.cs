using UnityEngine;
using UnityEngine.Serialization;

public class BuildingBlocker : MonoBehaviour
{
    public Sprite Sprite;

    [FormerlySerializedAs("IsIndestructable")]
    public bool IsIndestructible;

    private void Start()
    {
        Vector3  position = transform.position;
        int      ypos     = (int) position.y;
        int      xpos     = (int) position.x;
        Building b        = gameObject.AddComponent<Building>();
        b.IsIndestructable = IsIndestructible;
        //TODO: Don't love that this clogs up the _actual_ buildings because we do scan that whole list occasionally
        BuildingManager.Instance.ConstructedBuildings[new Vector2(xpos, ypos)] = b;

        SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
        renderer.sprite = Sprite;
        if (Sprite != null)
        {
            renderer.color = Color.white;
        }
    }
}