using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardList : MonoBehaviour
{
    public static CardList Instance;
    public Deck Commons;
    public Deck Uncommons;
    public Deck Rares;
    public Deck Epics;
    public Deck Legendaries;

    public Deck AllCards;

    private Dictionary<Card.CardRarity, Deck> _decksByRarity;

    public void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }

        _decksByRarity =
            new Dictionary<Card.CardRarity, Deck>()
            {
                {Card.CardRarity.Common, Commons},
                {Card.CardRarity.Uncommon, Uncommons},
                {Card.CardRarity.Rare, Rares},
                {Card.CardRarity.Epic, Epics},
                {Card.CardRarity.Legendary, Legendaries},
            };
    }

    public static Deck GetCardsByRarity(Card.CardRarity rarity)
    {
        return Instance._decksByRarity[rarity];
    }

    public static Card GetRandomCard()
    {
        int index = Random.Range(0, Instance.AllCards.Cards.Count);
        return Instance.AllCards.Cards[index];
    }

    public static Card GetRandomCardOfRarity(Card.CardRarity rarity)
    {
        int index = 0;
        switch (rarity)
        {
            case Card.CardRarity.Common:
                index = Random.Range(0, Instance.Commons.Cards.Count);
                return Instance.Commons.Cards[index];

            case Card.CardRarity.Uncommon:
                index = Random.Range(0, Instance.Uncommons.Cards.Count);
                return Instance.Uncommons.Cards[index];

            case Card.CardRarity.Rare:
                index = Random.Range(0, Instance.Rares.Cards.Count);
                return Instance.Rares.Cards[index];

            case Card.CardRarity.Epic:
                index = Random.Range(0, Instance.Epics.Cards.Count);
                return Instance.Epics.Cards[index];

            case Card.CardRarity.Legendary:
                index = Random.Range(0, Instance.Legendaries.Cards.Count);
                return Instance.Legendaries.Cards[index];

            default:
                throw new ArgumentOutOfRangeException(nameof(rarity), rarity, null);
        }
    }
}