using System;
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
    public GameObject CostContainer;
    public Image Artwork;
    public Image ArtworkBackground;
    public Image CardBorder;
    public TextMeshProUGUI InstructionText;
    
    public Color32 CommonColor;
    public Color32 RareColor;
    public Color32 EpicColor;
    public Color32 LegendaryColor;
    public Color32 BackgroundTint;
    
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
        Name.text = CardObject.Name;
        Cost.text = CardObject.CastingCost.ToString();
        if (CardObject.CastingCost == 0)
        {
            CostContainer.SetActive(false);
        }

        InstructionText.text = CardObject.InstructionText;
        Artwork.sprite       = CardObject.Artwork;

        //TODO: This is just a temporary quickfix to make cards without assigned artwork easier to identify;
        if (Artwork.sprite == null)
        {
            int     nameToInt = CardObject.Name[0] + CardObject.Name[CardObject.Name.Length - 1];
            byte    r         = (byte) (nameToInt * 13  % 255);
            byte    g         = (byte) (nameToInt / 23  % 255);
            byte    b         = (byte) (nameToInt * 113 % 255);
            Color32 col       = new Color32(r, g, b, 255);
            Artwork.color = col;
        }

        RectTransform artworkTransform = Artwork.gameObject.GetComponent<RectTransform>();
        //TODO: targetting position here is screwy when the game scales in screen size. Maybe _just_ in editor though?
        artworkTransform.position = new Vector3(
            artworkTransform.position.x + CardObject.ArtworkOffset.x,
            artworkTransform.position.y + CardObject.ArtworkOffset.y,
            0
        );
        artworkTransform.localScale = new Vector3(CardObject.ArtworkScale.x, CardObject.ArtworkScale.y, 1);

        //artworkTransform.localPosition = artworkTransform.localPosition;
        //artworkTransform.localScale = artworkTransform.localScale;
        /*if (CardObject.ArtworkBackground != null)
        {
            ArtworkBackground.sprite = CardObject.ArtworkBackground;
        }*/

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

        //ArtworkBackground.color = BackgroundTint;
        gameObject.name = Name.text;
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        UpdateCardDetails();
    }
#endif
}