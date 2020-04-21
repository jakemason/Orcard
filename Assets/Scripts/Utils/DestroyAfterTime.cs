using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float DurationInSeconds = 1.0f;

    private void Update()
    {
        DurationInSeconds -= Time.deltaTime;
        if (DurationInSeconds <= 0.0f)
        {
            Destroy(gameObject);
        }
    }
}