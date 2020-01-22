using UnityEngine;

public class TowerBolt : MonoBehaviour
{
    public float MovementSpeed = 5.0f;
    public Vector3 Target;

    private void Update()
    {
        if (Vector3.Distance(Target, transform.position) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, Target, MovementSpeed * Time.deltaTime);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}