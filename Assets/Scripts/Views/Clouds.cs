using UnityEngine;

public class Clouds : MonoBehaviour
{
    public float ScrollSpeed = -.1f;
    public float Offset;
    private SpriteRenderer _renderer;

    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Offset += (Time.deltaTime * ScrollSpeed) / 10.0f;
        _renderer.material.SetTextureOffset("_MainTex", new Vector2(Offset, 0));
    }
}