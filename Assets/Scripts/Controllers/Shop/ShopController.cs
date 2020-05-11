using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public int RerollCost = 25;
    public static List<Card> OnSale = new List<Card>();
    public static float CostMultiplier = 1.0f; //cards can manipulate this to go for a "shop" strat

    public static int CardsToGenerate = 6;
    public static int CardsToDiscount = 2; // this many cards will get a random discount applied to them.
    private static float _discountMultiplier = 0.8f;

    private static void GenerateNewCardsForSale()
    {
        OnSale = new List<Card>();
        for (int i = 0; i < CardsToGenerate; i++)
        {
            OnSale.Add(CardList.GetRandomCard());
        }
    }

    public void Reroll()
    {
        if (IncomeController.GetCurrentGold() < RerollCost) return;
        GenerateNewCardsForSale();
        ShopView.CreateCards(OnSale);
        ShopView.RefreshView();
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