using UnityEngine;

[ExecuteInEditMode]
public class GridBasedPositioning : MonoBehaviour
{
    private void Update()
    {
        Vector3 pos     = transform.position;
        Vector3 rounded = new Vector3(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y), Mathf.RoundToInt(pos.z));
        transform.position = rounded;
    }
}