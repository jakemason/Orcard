using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
[RequireComponent(typeof(RectTransform))]
public class CardRenderer : MonoBehaviour
{
    // @formatter:off
    [Header("Card Data")] 
    public Card CardObject;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Cost;
    public Image Artwork;
    public Image Background;
    public TextMeshProUGUI InstructionText;
    
    private PlayableCardController _playable;
    // @formatter:on

    private static readonly Dictionary<Card.CardRarity, Color> RarityColors = new Dictionary<Card.CardRarity, Color>
    {
        {Card.CardRarity.Common, new Color32(71,     106, 111, 255)},
        {Card.CardRarity.Uncommon, new Color32(81,   158, 138, 255)},
        {Card.CardRarity.Rare, new Color32(126,      176, 155, 255)},
        {Card.CardRarity.Epic, new Color32(197,      201, 164, 255)},
        {Card.CardRarity.Legendary, new Color32(236, 190, 180, 255)},
    };

    private void Awake()
    {
        _playable = GetComponent<PlayableCardController>();
        if (_playable)
        {
            _playable.CardObject = CardObject;
        }

        UpdateCardDetails();
    }

    public void UpdateCardDetails()
    {
        if (CardObject == null) return;
        Name.text            = CardObject.Name;
        Cost.text            = CardObject.CastingCost.ToString();
        InstructionText.text = CardObject.InstructionText;
        Artwork.sprite       = CardObject.Artwork;

        gameObject.name  = Name.text;
        Background.color = RarityColors[CardObject.Rarity];
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        UpdateCardDetails();
    }
#endif
}