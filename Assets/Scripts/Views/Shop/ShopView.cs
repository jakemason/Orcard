using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopView : MonoBehaviour
{
    public static ShopView Instance;
    public GameObject ShopRoot;
    public GameObject CardGridRoot;
    public GameObject ShopCardPrefab;
    private GridLayoutGroup _layoutGroup;

    private void Awake()
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

    public static void Toggle()
    {
        if (Instance.ShopRoot.activeSelf)
        {
            Disable();
        }
        else
        {
            Enable();
        }
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

    public static void RefreshView()
    {
        Instance._layoutGroup.enabled = false;
        Canvas.ForceUpdateCanvases();
        Instance._layoutGroup.enabled = true;
    }

    public static bool IsEnabled()
    {
        return Instance.ShopRoot.activeSelf;
    }

    //TODO: Investigate why the hell sometimes the artwork is offset on the first page load of the Shop, rerolls are fine...
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
        }

        RefreshView();
    }
}