using Players;
using UnityEngine;

[CreateAssetMenu(fileName = "New Requires Card Target", menuName = "Requirements/Requires Card Target")]
public class RequiresCardTarget : Requirement
{
    public override bool RequirementMet()
    {
        Debug.Log("Checking card target: " + SpellCast.CardTarget);
        return (PlayableCardController) SpellCast.CardTarget != null &&
               (PlayableCardController) SpellCast.CardTarget != SpellCast.LastCardPlayed;
    }
}