using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(Image))]
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
    public TextMeshProUGUI Attack;
    public TextMeshProUGUI Health;
    public TextMeshProUGUI Armor;
    public TextMeshProUGUI CardType;
    
    private PlayableCardController _playable;

    // @formatter:on
    private static readonly Dictionary<Card.CardRarity, Color> RarityColors = new Dictionary<Card.CardRarity, Color>
    {
        {Card.CardRarity.Common, Color.gray},
        {Card.CardRarity.Uncommon, Color.cyan},
        {Card.CardRarity.Rare, Color.blue},
        {Card.CardRarity.Epic, Color.magenta},
        {Card.CardRarity.Legendary, Color.yellow},
    };

    private void Awake()
    {
        UpdateCardDetails();
        _playable = GetComponent<PlayableCardController>();
        if (_playable)
        {
            _playable.CardObject = CardObject;
        }
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