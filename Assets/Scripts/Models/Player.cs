using System.Collections.Generic;
using TMPro;
using UnityEngine;

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
        public List<Card> DiscardPile;
        public int CardsToDrawEachTurn = 3;

        [Header("Player Energy")]
        [Space(20)]
        private int _remainingEnergy;
        public int EnergyGainedPerTurn = 3;
        public int MaxEnergy = 10;
        // @formatter:on 
        public int RemainingEnergy
        {
            get => _remainingEnergy;
            set
            {
                _remainingEnergy = value;
                if (_remainingEnergy > MaxEnergy)
                {
                    _remainingEnergy = MaxEnergy;
                }

                UpdateEnergyCounter();
            }
        }
        // @formatter:off

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
        //public GameObject TargettingIndicator;
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

            DeckForCurrentRun = Instantiate(DeckTemplate);
            DeckForCurrentRun.Cards.Shuffle();
        }

        private void Start()
        {
            StartTurn();
        }

        public static void EndTurn()
        {
            PlayerHand.DiscardHand();
        }

        public static void StartTurn()
        {
            Instance.RefillEnergy();
            Instance.Draw(Instance.CardsToDrawEachTurn);
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }

        public void RefillEnergy()
        {
            RemainingEnergy += EnergyGainedPerTurn;
        }

        public static void ModifyEnergy(int modifier)
        {
            Instance.RemainingEnergy += modifier;
        }

        public static bool HasEnergy()
        {
            return Instance.RemainingEnergy > 0;
        }

        public static int GetEnergy()
        {
            return Instance._remainingEnergy;
        }

        public static void UpdateEnergyCounter()
        {
            Instance.EnergyCounter.text = "Energy: " + Instance._remainingEnergy + " / " + Instance.MaxEnergy;
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
        }

        /// <summary>
        /// Draws a single card from the player's deck, shuffling the discard pile into the deck
        /// if needed.
        /// </summary>
        private void DrawSingleCard()
        {
            if (DeckForCurrentRun.Cards.Count <= 0) ShuffleDiscardIntoDeck();
            if (DeckForCurrentRun.Cards.Count <= 0) return;

            Card card = Instantiate(DeckForCurrentRun.Cards[0]);
            DeckForCurrentRun.Cards.RemoveAt(0);

            foreach (Effect effect in card.OnDrawEffects)
            {
                effect.Activate();
            }

            GameObject drawnCard = Instantiate(CardRendererTemplate, HandGameObject.transform);
            drawnCard.transform.position = DeckPosition.position;
            PlayerHand.Instance.AddCardToHand(drawnCard);
            CardRenderer drawnCardRenderer = drawnCard.GetComponent<CardRenderer>();
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

            DeckForCurrentRun.Cards.AddRange(DiscardPile);
            DiscardPile.Clear();
            DeckForCurrentRun.Cards.Shuffle();
        }
    }
}