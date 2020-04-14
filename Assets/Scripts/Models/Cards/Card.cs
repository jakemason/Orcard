using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class Card : ScriptableObject
{
    public enum CardRarity
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary,
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
    public Sprite ArtworkBackground;
    public CardRarity Rarity = CardRarity.Common;
    public CardSet Set = CardSet.Basic;
    public int CastingCost;
    public int GoldCost;
    public List<Requirement> CastingRequirements;
    // if this card should "burn" after being played so it cannot be drawn again.
    public bool DestroyOnCast = false;
    public bool IsTargeted = false;
    
    [Header("Card Instruction Text")]
    [Space(20)]
    [TextArea]  public string InstructionText;
    [Tooltip("Additional text that is appended to the default instruction text.")]
    [TextArea] public string AdditionalInstructionText;
    [TextArea] public string OverrideDefaultInstructionText;
    [TextArea] public string FlavorText;
    
    [Header("Card Effects")]
    [Space(20)]
    [Tooltip("Any effects the card has when the player draws them.")]
    public List<Effect> OnDrawEffects;
    [Tooltip("The immediate effects of the card. Example: Deal 2 damage to target.")]
    public List<Effect> ImmediateEffects;
    // @formatter:on 

#if UNITY_EDITOR
    public virtual void OnValidate()
    {
        // this makes the editor typing experience feel a little weird, but saves us from having to manually
        // rename the file to match the name of the card. I feel this is useful enough to keep for now.
        string playEffects = "";
        if (ImmediateEffects.Count > 0 && OverrideDefaultInstructionText == "")
        {
            foreach (Effect effect in ImmediateEffects)
            {
                if (effect == null || effect.InstructionText == "") continue;
                playEffects += effect.InstructionText + " ";
            }
        }

        string startEffects = "";
        /*if (PermanentStartOfTurnEffects.Count > 0 && OverrideDefaultInstructionText == "")
        {
            startEffects = "Each turn ";
            foreach (Effect effect in PermanentStartOfTurnEffects)
            {
                if (effect == null) continue;
                startEffects += effect.InstructionText + " ";
            }
        }

        string nextTurnEffects = "";
        if (StartOfNextTurnEffects.Count > 0 && OverrideDefaultInstructionText == "")
        {
            nextTurnEffects = "Next turn ";
            foreach (Effect effect in StartOfNextTurnEffects)
            {
                if (effect == null) continue;
                nextTurnEffects += effect.InstructionText + " ";
            }
        }*/

        InstructionText = playEffects + startEffects; // + nextTurnEffects;
        if (DestroyOnCast)
        {
            InstructionText += "Destroy this card. ";
        }

        if (OverrideDefaultInstructionText != "")
        {
            InstructionText = OverrideDefaultInstructionText;
        }

        string assetPath = AssetDatabase.GetAssetPath(GetInstanceID());
        AssetDatabase.RenameAsset(assetPath, Name);
    }
#endif
}