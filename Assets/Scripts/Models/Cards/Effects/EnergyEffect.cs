using Players;
using UnityEngine;

[CreateAssetMenu(fileName = "New Energy Effect", menuName = "Effects/Energy Effect")]
public class EnergyEffect : Effect
{
    public int EnergyModifier = 1;

    public override void Activate()
    {
        Player.Instance.RemainingEnergy += EnergyModifier;
    }

    public void OnValidate()
    {
        InstructionText = EnergyModifier > 0 ? $"+{EnergyModifier} Energy." : $"-{EnergyModifier} Energy.";
    }
}