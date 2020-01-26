using Players;
using UnityEngine;

[CreateAssetMenu(fileName = "New Energy Effect", menuName = "Effects/Energy Effect")]
public class EnergyEffect : Effect
{
    public int EnergyModifier = 0;

    public override void Activate()
    {
        Player.Instance.RemainingEnergy += EnergyModifier;
    }
}