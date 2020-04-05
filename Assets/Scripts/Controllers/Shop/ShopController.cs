using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public static List<Card> OnSale;
    public static float CostMultiplier = 1.0f; //cards can manipulate this to go for a "shop" strat

    public static int CardsToGenerate = 8;
    public static int CardsToDiscount = 2; // this many cards will get a random discount applied to them.
    private static float _discountMultiplier = 0.8f;

    public static void GenerateNewCardsForSale()
    {
    }

    public static void OpenShop()
    {
    }

    public static void CloseShop()
    {
    }
}