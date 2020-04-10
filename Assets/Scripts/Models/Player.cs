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
        
        [Header("Delayed Effects")]
        [Space(20)]
        public List<Effect> TemporaryOnTurnStartEffects = new List<Effect>();
        public List<Effect> PermanentOnTurnStartEffects = new List<Effect>();
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

        [Header("Player Gold")]
        [Space(20)]
        public int CurrentGold = 0;
        public TextMeshProUGUI GoldText;

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

            DeckForCurrentRun = Instantiate(DeckTemplate);
            DeckForCurrentRun.Cards.Shuffle();
        }

        private void Start()
        {
            StartTurn();
            ModifyGold(0);
        }

        public static void EndTurn()
        {
            PlayerHand.DiscardHand();
        }

        public static void StartTurn()
        {
            Instance.RefillEnergy();
            Instance.Draw(Instance.CardsToDrawEachTurn);

            foreach (Effect effect in Instance.PermanentOnTurnStartEffects)
            {
                effect.Activate();
            }

            foreach (Effect effect in Instance.TemporaryOnTurnStartEffects)
            {
                effect.Activate();
            }

            Instance.TemporaryOnTurnStartEffects = new List<Effect>();
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }

        public static void RegisterTemporaryOnTurnEffect(Effect toRegister)
        {
            Instance.TemporaryOnTurnStartEffects.Add(toRegister);
        }

        public static void RegisterPermanentOnTurnEffect(Effect toRegister)
        {
            Instance.PermanentOnTurnStartEffects.Add(toRegister);
        }

        public void RefillEnergy()
        {
            RemainingEnergy = EnergyGainedPerTurn;
        }

        public static void ModifyGold(int modifier)
        {
            Instance.CurrentGold += modifier;
            if (Instance.CurrentGold < 0)
            {
                Instance.CurrentGold = 0;
            }

            Instance.GoldText.text = Instance.CurrentGold.ToString();
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
            Instance.EnergyCounter.text = Instance._remainingEnergy.ToString();
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