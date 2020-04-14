﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopView : MonoBehaviour
{
    public static ShopView Instance;
    public GameObject ShopRoot;
    public GameObject CardGridRoot;
    public GameObject ShopCardPrefab;
    private GridLayoutGroup _layoutGroup;

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

        _layoutGroup = ShopRoot.GetComponentInChildren<GridLayoutGroup>();
    }


    public static void Enable()
    {
        Instance.ShopRoot.SetActive(true);
        Canvas.ForceUpdateCanvases();
        Instance._layoutGroup.enabled = false;
    }

    public static void Disable()
    {
        Instance.ShopRoot.SetActive(false);
        Instance._layoutGroup.enabled = true;
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