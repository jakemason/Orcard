using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class RewardsManager : MonoBehaviour
{
    // @formatter:off 
    public static RewardsManager Instance;
    public static bool IsOpen;
    [Header("Rewards Offered")]
    [Range(0,1)] public float ChanceForRandomRarityUpgrade = 0.2f;
    public int NumberOfRewardChoices = 3;
    
    [Header("UI")]
    [Space(20)]
    public Button Button;
    
    [Header("Prefabs and Holders")]
    [Space(20)]
    public GameObject RewardsPanel;
    public GameObject CardSelectionBox;
    public GameObject CardRendererPrefab;
    [ReadOnly] private List<GameObject> _rewardsOfferedGameObjects;
    [ReadOnly] private List<Card> _rewardsOffered;
    // @formatter:on 

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

        _rewardsOfferedGameObjects = new List<GameObject>();
        _rewardsOffered            = new List<Card>();
    }

    public static void MakeNewRewardAvailable()
    {
//        Instance.Button.interactable = true;
        //       Instance.CreateRewardGameObjects();
    }

    public void OpenRewardsPanel()
    {
        IsOpen = true;
        RewardsPanel.SetActive(true);
    }

    public static void CloseRewardsPanel()
    {
        Instance.ClearRewardGameObjects();
        IsOpen                       = false;
        Instance.Button.interactable = false;
    }

    private void PickRewards()
    {
        for (int i = 0; i < NumberOfRewardChoices; i++)
        {
            Card.CardRarity rarity     = Card.CardRarity.Common;
            float           cardRarity = Random.Range(0.0f, 1.0f);
            if (cardRarity >= 0.95f)
            {
                rarity = Card.CardRarity.Legendary;
            }
            else if (cardRarity >= 0.9f)
            {
                rarity = Card.CardRarity.Epic;
            }
            else if (cardRarity >= 0.7f)
            {
                rarity = Card.CardRarity.Rare;
            }
            else if (cardRarity >= 0.5f)
            {
                rarity = Card.CardRarity.Uncommon;
            }
            else if (cardRarity >= 0.3f)
            {
                rarity = Card.CardRarity.Common;
            }

            Deck setToPullFrom = CardList.GetCardsByRarity(rarity);

            int cardIndex = Random.Range(0, setToPullFrom.Cards.Count);
            _rewardsOffered.Add(setToPullFrom.Cards[cardIndex]);
        }
    }

    private void PickRewardsOfRarity(Card.CardRarity rarity)
    {
        for (int i = 0; i < NumberOfRewardChoices; i++)
        {
            Deck setToPullFrom = CardList.GetCardsByRarity(rarity);

            int cardIndex = Random.Range(0, setToPullFrom.Cards.Count);
            _rewardsOffered.Add(setToPullFrom.Cards[cardIndex]);
        }
    }


    private void CreateRewardGameObjects()
    {
        ClearRewardGameObjects();
        PickRewards();


        for (int i = 0; i < _rewardsOffered.Count; i++)
        {
            GameObject   reward = Instantiate(CardRendererPrefab, CardSelectionBox.transform, false);
            CardRenderer rend   = reward.GetComponent<CardRenderer>();
            rend.CardObject = _rewardsOffered[i];
            rend.UpdateCardDetails();
            _rewardsOfferedGameObjects.Add(reward);
        }
    }

    /// <summary>
    /// Called from Unity button
    /// </summary>
    [UsedImplicitly]
    public void SkipReward()
    {
        CloseRewardsPanel();
    }

    private void ClearRewardGameObjects()
    {
        for (int i = _rewardsOfferedGameObjects.Count - 1; i >= 0; i--)
        {
            GameObject go = _rewardsOfferedGameObjects[i];
            Destroy(go.gameObject);
            _rewardsOfferedGameObjects.RemoveAt(i);
        }

        _rewardsOffered.Clear();
        RewardsPanel.SetActive(false);
    }
}