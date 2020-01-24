using UnityEngine;

[CreateAssetMenu(fileName = "New Tower", menuName = "Towers/Tower")]
public class TowerModel : ScriptableObject
{
    public GameObject BoltPrefab;
    public int Damage = 1;
    public float AttackRate = 0.25f;
    public float AttackMovementSpeed = 10f;
    public float Range = 5f;
}