using System;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public int ShopRefreshInSeconds = 60;
    [SerializeField] private float _shopRefreshTimer;
    public static List<Card> OnSale;
    public static float CostMultiplier = 1.0f; //cards can manipulate this to go for a "shop" strat

    public static int CardsToGenerate = 6;
    public static int CardsToDiscount = 2; // this many cards will get a random discount applied to them.
    private static float _discountMultiplier = 0.8f;

    private void Start()
    {
        _shopRefreshTimer = ShopRefreshInSeconds;
    }

    public void Update()
    {
        _shopRefreshTimer -= Time.deltaTime;
        if (_shopRefreshTimer <= 0f && !ShopView.IsEnabled())
        {
            GenerateNewCardsForSale();
            ShopView.CreateCards(OnSale);
        }
    }

    private static void GenerateNewCardsForSale()
    {
        OnSale = new List<Card>();
        for (int i = 0; i < CardsToGenerate; i++)
        {
            OnSale.Add(CardList.GetRandomCard());
        }
    }

    /// <summary>
    /// Just used for testing via a button
    /// TODO: Kill this function when no longer useful
    /// </summary>
    public void Open()
    {
        ShopView.Enable();
    }


    /// <summary>
    /// Just used for testing via a button
    /// TODO: Kill this function when no longer useful
    /// </summary>
    public void Close()
    {
        ShopView.Disable();
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