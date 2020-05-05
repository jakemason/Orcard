using UnityEngine;

[ExecuteInEditMode]
public class GridBasedPositioning : MonoBehaviour
{
#if (UNITY_EDITOR) //we just want this while editing, it doesn't need to run in builds.
    private void LateUpdate()
    {
        Vector3 pos     = transform.position;
        Vector3 rounded = new Vector3(Mathf.RoundToInt(pos.x), Mathf.RoundToInt(pos.y), Mathf.RoundToInt(pos.z));
        transform.position = rounded;
    }
#endif
}