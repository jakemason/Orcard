using UnityEngine;

public class TowerAttack : MonoBehaviour
{
    public float MovementSpeed = 5.0f;
    public int Damage = 1;
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

    public void OnTriggerEnter2D(Collider2D col)
    {
        Health hp = col.gameObject.GetComponent<Health>();
        hp.TakeDamage(Damage);
        Destroy(gameObject);
    }
}