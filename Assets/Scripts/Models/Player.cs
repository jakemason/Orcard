using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Players
{
    public class Player : MonoBehaviour, ITargetable
    {
        public static Player Instance;

        // @formatter:off
        [Header("Player Card Info")]
        [Space(20)]
        public Deck DeckTemplate;
        public Deck DeckForCurrentRun;
        public Deck DeckForCurrentEncounter;
        public List<Card> DiscardPile;
        public int CardsToDrawEachTurn = 3;
        
        [Header("Player Energy")] 
        [Space(20)]
        public int MaximumEnergy = 4;
        public int RemainingEnergy;
        
        [Header("Player Health")]
        [Space(20)]
        public int StartingHealth = 55;
        public Health Health;

        [Header("Visuals")]
        [Space(20)]
        public Transform DeckPosition;
        public GameObject HandGameObject;
        public GameObject CardRendererTemplate;
        public TextMeshProUGUI EnergyCounter;
        public TextMeshProUGUI DeckCount;
        // @formatter:on

        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(this);
            }

            Health = GetComponent<Health>();
            Health.SetStartingHealth(StartingHealth);

            DeckForCurrentEncounter = Instantiate(DeckTemplate);
            DeckForCurrentRun       = Instantiate(DeckTemplate);
            DeckForCurrentRun.Cards.Shuffle();
            DeckForCurrentEncounter.Cards.Shuffle();
            RemainingEnergy = MaximumEnergy;
            UpdateEnergyCounter();
        }

        private void Start()
        {
            StartTurn();
        }

        public static void StartNewBattle()
        {
            Instance.DeckForCurrentEncounter = Instance.DeckForCurrentRun;
            Instance.DeckForCurrentEncounter.Cards.Shuffle();
            Instance.StartTurn();
        }

        public void EndTurn()
        {
            PlayerHand.DiscardHand();
            StartTurn();
        }

        public void StartTurn()
        {
            RefillEnergy();
            Draw(CardsToDrawEachTurn);
        }

        public void Update()
        {
            //TODO: Find a better way to handle this than Update()
            if (Health.CurrentHealth <= 0)
            {
                SceneManager.LoadScene("You Died");
            }
        }

        public void RefillEnergy()
        {
            RemainingEnergy = MaximumEnergy;
            UpdateEnergyCounter();
        }

        public void UpdateEnergyCounter()
        {
            EnergyCounter.text = Instance.RemainingEnergy.ToString();
        }

        /// <summary>
        /// Draws X cards from the Player's deck. 
        /// </summary>
        /// <param name="cardsToDraw">The number of cards to draw</param>
        public void Draw(int cardsToDraw)
        {
            for (int i = 0; i < cardsToDraw; i++)
            {
                DrawSingleCard();
            }

            DeckCount.text = DeckForCurrentEncounter.Cards.Count.ToString();
        }

        /// <summary>
        /// Draws a single card from the player's deck, shuffling the discard pile into the deck
        /// if needed.
        /// </summary>
        private void DrawSingleCard()
        {
            if (DeckForCurrentEncounter.Cards.Count <= 0) ShuffleDiscardIntoDeck();
            if (DeckForCurrentEncounter.Cards.Count <= 0) return;

            Card card = DeckForCurrentEncounter.Cards[0];
            DeckForCurrentEncounter.Cards.RemoveAt(0);

            foreach (Effect effect in card.OnDrawEffects)
            {
                effect.Activate();
            }

            GameObject drawnCard = Instantiate(CardRendererTemplate, HandGameObject.transform);
            drawnCard.transform.position = DeckPosition.position;
            PlayerHand.Instance.AddCardToHand(drawnCard);
            CardRenderer drawnCardRenderer = drawnCard.GetComponent<CardRenderer>();
            Debug.Assert(drawnCardRenderer != null);
            drawnCardRenderer.CardObject = card;
            drawnCardRenderer.UpdateCardDetails();
        }

        /// <summary>
        /// Shuffles the player's discard pile into their hand.
        /// Unsure if we'll keep this mechanic or not.
        /// </summary>
        private void ShuffleDiscardIntoDeck()
        {
            if (DiscardPile.Count <= 0)
            {
                return;
            }

            DeckForCurrentEncounter.Cards.AddRange(DiscardPile);
            DiscardPile.Clear();
            DeckForCurrentEncounter.Cards.Shuffle();
            DeckCount.text = DeckForCurrentEncounter.Cards.Count.ToString();
        }
    }
}