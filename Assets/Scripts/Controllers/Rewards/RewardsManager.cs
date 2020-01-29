using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RewardsManager : MonoBehaviour
{
    // @formatter:off 
    public static RewardsManager Instance;
    [Header("Rewards Offered")]
    [Range(0,1)] public float ChanceForRandomRarityUpgrade = 0.2f;
    public int NumberOfRewardChoices = 3;
    public Deck Commons;
    public Deck Uncommons;
    public Deck Rares;
    public Deck Epics;
    public Deck Legendaries;
    
    [Header("Prefabs and Holders")]
    [Space(20)]
    public GameObject RewardsPanel;
    public GameObject CardSelectionBox;
    public GameObject CardRendererPrefab;
    [ReadOnly] private List<GameObject> _rewardsOfferedGameObjects;
    [ReadOnly] private List<Card> _rewardsOffered;
    // @formatter:on 

    private Dictionary<Card.CardRarity, Card.CardRarity> _rarityUpgrades;
    private Dictionary<Card.CardRarity, Deck> _rewardTiers;

    public static void OpenRewardsPanel()
    {
        Instance.CreateRewardGameObjects();
    }

    public static void CloseRewardsPanel()
    {
        Instance.ClearRewardGameObjects();
    }

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

        SetupDecks();
    }

    private void SetupDecks()
    {
        //TODO: Can we just increment the enum instead and just make sure we keep them in order?
        //TODO: Make an upgrade method of some sort?
        _rarityUpgrades =
            new Dictionary<Card.CardRarity, Card.CardRarity>()
            {
                {Card.CardRarity.Common, Card.CardRarity.Uncommon},
                {Card.CardRarity.Uncommon, Card.CardRarity.Rare},
                {Card.CardRarity.Rare, Card.CardRarity.Epic},
                {Card.CardRarity.Epic, Card.CardRarity.Legendary},
                {Card.CardRarity.Legendary, Card.CardRarity.Legendary} //safety check
            };

        _rewardTiers =
            new Dictionary<Card.CardRarity, Deck>()
            {
                {Card.CardRarity.Common, Commons},
                {Card.CardRarity.Uncommon, Uncommons},
                {Card.CardRarity.Rare, Rares},
                {Card.CardRarity.Epic, Epics},
                {Card.CardRarity.Legendary, Legendaries},
            };
    }

    private void PickRewards()
    {
        for (int i = 0; i < NumberOfRewardChoices; i++)
        {
            Card.CardRarity rarity         = Card.CardRarity.Common;
            float           rollForUpgrade = Random.Range(0.0f, 1.0f);
            if (rollForUpgrade <= ChanceForRandomRarityUpgrade)
            {
                rarity = _rarityUpgrades[rarity];
            }

            Deck setToPullFrom = _rewardTiers[rarity];

            int cardIndex = Random.Range(0, setToPullFrom.Cards.Count);
            _rewardsOffered.Add(setToPullFrom.Cards[cardIndex]);
        }
    }


    private void CreateRewardGameObjects()
    {
        PickRewards();
        for (int i = 0; i < _rewardsOffered.Count; i++)
        {
            GameObject   reward = Instantiate(CardRendererPrefab, CardSelectionBox.transform, false);
            CardRenderer rend   = reward.GetComponent<CardRenderer>();
            rend.CardObject = _rewardsOffered[i];
            rend.UpdateCardDetails();
            _rewardsOfferedGameObjects.Add(reward);
        }

        RewardsPanel.SetActive(true);
    }

    public void SkipReward()
    {
        ClearRewardGameObjects();
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