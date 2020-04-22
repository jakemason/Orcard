using UnityEngine;

public class EnemyDeathController : MonoBehaviour
{
    public Enemy Model;
    [Header("Orc Accessories")] public SpriteRenderer Sword;
    public SpriteRenderer SmallHelm;
    public SpriteRenderer LargeHelm;
    public SpriteRenderer Shield;
    public SpriteRenderer Boots;
    public SpriteRenderer SmallShoulders;
    public SpriteRenderer LargeShoulders;
    public SpriteRenderer WarBanner;

    public void Activate(Enemy model)
    {
        Sword.enabled          = model.HasSword;
        SmallHelm.enabled      = model.HasSmallHelm;
        LargeHelm.enabled      = model.HasLargeHelm;
        Shield.enabled         = model.HasShield;
        Boots.enabled          = model.HasBoots;
        SmallShoulders.enabled = model.HasSmallShoulders;
        LargeShoulders.enabled = model.HasLargeShoulders;
        WarBanner.enabled      = model.HasWarBanner;
    }
}