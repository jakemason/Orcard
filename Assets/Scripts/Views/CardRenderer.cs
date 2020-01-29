using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
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
    [FormerlySerializedAs("Background")]public Image RarityBorder;
    public TextMeshProUGUI InstructionText;
    
    private PlayableCardController _playable;
    // @formatter:on

    private static readonly Dictionary<Card.CardRarity, Color> RarityColors = new Dictionary<Card.CardRarity, Color>
    {
        {Card.CardRarity.Common, Color.white},
        {Card.CardRarity.Uncommon, Color.grey},
        {Card.CardRarity.Rare, Color.blue},
        {Card.CardRarity.Epic, Color.magenta},
        {Card.CardRarity.Legendary, Color.yellow},
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

        gameObject.name    = Name.text;
        RarityBorder.color = RarityColors[CardObject.Rarity];
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        UpdateCardDetails();
    }
#endif
}