using System;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    private static ShopController _instance;
    public List<Card> AlwaysOnSale;
    public int RerollCost = 25;
    public static List<Card> OnSale = new List<Card>();
    public static float CostMultiplier = 1.0f; //cards can manipulate this to go for a "shop" strat

    public static int CardsToGenerate = 3;
    public static int CardsToDiscount = 2; // this many cards will get a random discount applied to them.
    private static float _discountMultiplier = 0.8f;

    public void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private static void GenerateNewCardsForSale()
    {
        OnSale = new List<Card>();
        foreach (Card c in _instance.AlwaysOnSale)
        {
            OnSale.Add(c);
        }

        for (int i = 0; i < CardsToGenerate; i++)
        {
            Card toAdd = CardList.GetRandomCard();
            while (_instance.AlwaysOnSale.Contains(toAdd))
            {
                toAdd = CardList.GetRandomCard();
            }

            OnSale.Add(toAdd);
        }
    }

    public void Reroll()
    {
        if (IncomeController.GetCurrentGold() < RerollCost) return;
        GenerateNewCardsForSale();
        ShopView.CreateCards(OnSale);
        IncomeController.ModifyGold(-RerollCost);
    }

    /// <summary>
    /// Just used for testing via a button
    /// TODO: Kill this function when no longer useful
    /// </summary>
    public void Open()
    {
        if (OnSale.Count == 0)
        {
            GenerateNewCardsForSale();
            ShopView.CreateCards(OnSale);
        }

        ShopView.Enable();
    }


    /// <summary>
    /// Just used for testing via a button
    /// TODO: Kill this function when no longer useful
    /// </summary>
    public void Close()
    {
        if (OnSale.Count == 0)
        {
            GenerateNewCardsForSale();
            ShopView.CreateCards(OnSale);
        }

        ShopView.Disable();
    }

    public void Toggle()
    {
        if (OnSale.Count == 0)
        {
            GenerateNewCardsForSale();
            ShopView.CreateCards(OnSale);
        }

        ShopView.Toggle();
    }

    public static void OpenShop()
    {
        ShopView.Enable();
    }

    public static void CloseShop()
    {
        ShopView.Disable();
    }
}