using System.Collections.Generic;
using UnityEngine;

public class ShopView : MonoBehaviour
{
    public static ShopView Instance;
    public GameObject ShopRoot;
    public GameObject CardGridRoot;
    public GameObject ShopCardPrefab;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }


    public static void Enable()
    {
        Instance.ShopRoot.SetActive(true);
    }

    public static void Disable()
    {
        Instance.ShopRoot.SetActive(false);
    }

    public static bool IsEnabled()
    {
        return Instance.ShopRoot.activeSelf;
    }

    public static void CreateCards(List<Card> toCreate)
    {
        foreach (Transform child in Instance.CardGridRoot.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Card card in toCreate)
        {
            GameObject   cardObject = Instantiate(Instance.ShopCardPrefab, Instance.CardGridRoot.transform, false);
            CardRenderer rend       = cardObject.GetComponent<CardRenderer>();
            rend.CardObject = card;
            rend.UpdateCardDetails();
        }
    }
}