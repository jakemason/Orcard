using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
[RequireComponent(typeof(RectTransform))]
public class CardRenderer : MonoBehaviour, ITargetable
{
    // @formatter:off
    [Header("Card Data")] 
    public Card CardObject;
    public TextMeshProUGUI Name;
    public TextMeshProUGUI Cost;
    public Image Artwork;
    public Image ArtworkBackground;
    public Image CardBorder;
    public TextMeshProUGUI InstructionText;
    
    public Color32 CommonColor;
    public Color32 RareColor;
    public Color32 EpicColor;
    public Color32 LegendaryColor;
    
    private PlayableCardController _playable;
    // @formatter:on

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
        if (CardObject.ArtworkBackground != null)
        {
            ArtworkBackground.sprite = CardObject.ArtworkBackground;
        }

        switch (CardObject.Rarity)
        {
            case Card.CardRarity.Common:
                CardBorder.color = CommonColor;
                break;
            case Card.CardRarity.Uncommon:
                CardBorder.color = CommonColor;
                break;
            case Card.CardRarity.Rare:
                CardBorder.color = RareColor;
                break;
            case Card.CardRarity.Epic:
                CardBorder.color = EpicColor;
                break;
            case Card.CardRarity.Legendary:
                CardBorder.color = LegendaryColor;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        gameObject.name = Name.text;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        UpdateCardDetails();
    }
#endif
}