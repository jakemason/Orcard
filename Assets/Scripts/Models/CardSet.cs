using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "New Card Set", menuName = "Card Set")]
public class CardSet : Deck
{
    // @formatter:off
    [Header("Rarity Filter")]
    [Space(20)]
    public bool FilterByRarity = false;
    public Card.CardRarity RarityToPull = Card.CardRarity.Common;

    [Header("Set Filter")]
    [Space(20)]
    public bool FilterByCardSet = false;
    public Card.CardSet SetToPull = Card.CardSet.Basic;
    // @formatter:on

    private void OnValidate()
    {
        Cards = new List<Card>();
        Card[] cards = Resources.LoadAll("Data/Cards", typeof(Card)).Cast<Card>().ToArray();

        foreach (Card card in cards)
        {
            if (FilterByRarity && FilterByCardSet)
            {
                if (RarityToPull == card.Rarity && SetToPull == card.Set)
                {
                    Cards.Add(card);
                }
            }
            else if (FilterByRarity && !FilterByCardSet)
            {
                if (RarityToPull == card.Rarity)
                {
                    Cards.Add(card);
                }
            }
            else if (!FilterByRarity && FilterByCardSet)
            {
                if (SetToPull == card.Set)
                {
                    Cards.Add(card);
                }
            }
            else
            {
                Cards.Add(card);
            }
        }
    }
}