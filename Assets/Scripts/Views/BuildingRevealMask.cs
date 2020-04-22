using UnityEngine;

public class BuildingRevealMask : MonoBehaviour
{
    public float RevealSpeed = 1.0f;

    private void Update()
    {
        Vector3 localScale = transform.localScale;
        localScale.y         += RevealSpeed * Time.deltaTime;
        transform.localScale =  localScale;
        if (localScale.y >= 8.0f)
        {
            Destroy(this);
        }
    }
}