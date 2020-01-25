using UnityEngine;

[CreateAssetMenu(fileName = "New Tower Card", menuName = "Cards/Tower Card")]
//TODO: We can probably move all of TowerModel into here now
public class TowerCard : Card
{
    public GameObject BoltPrefab;
    public int Damage = 1;
    public float AttackRate = 0.25f;
    public float AttackMovementSpeed = 10f;
    public float Range = 5f;
}