using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class Card : ScriptableObject
{
    public enum CardRarity
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary
    }

    public enum CardSet
    {
        Basic,
        Warfare,
        Bulwark,
        Holy,
        Fire,
        Developer
    }

    // @formatter:off
    [Header("Card Info")] 
    public string Name;
    public Sprite Artwork;
    public CardRarity Rarity = CardRarity.Common;
    public CardSet Set = CardSet.Basic;
    public int CastingCost;
    public List<CastingRequirement> CastingRequirements;
    // if this card should "burn" after being played so it cannot be drawn again.
    public bool DestroyOnCast = false;
    
    [Header("Card Instruction Text")]
    [Space(20)]
    public string InstructionText;
    [Tooltip("Additional text that is appended to the default instruction text.")]
    public string AdditionalInstructionText;
    public string OverrideDefaultInstructionText;
    public string FlavorText;
    
    [Header("Card Effects")]
    [Space(20)]
    [Tooltip("Any effects the card has when the player draws them.")]
    public List<Effect> OnDrawEffects;
    [Tooltip("The immediate effects of the card. Example: Deal 2 damage to target.")]
    public List<Effect> PlayEffects;
    // @formatter:on 
}