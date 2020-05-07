using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Players
{
    public class PlayerController : MonoBehaviour, ITargetable
    {
        public static PlayerController Instance;

        // @formatter:off
        [Header("Player Card Info")] 
        [Space(20)]
        public Deck DeckTemplate;
        public Deck DeckForCurrentRun;
        public List<Card> DiscardPile;
        
        [Header("Player Draws")] 
        [Space(20)]
        public int CardsToDraw = 3;
        public int RedrawGoldCost = 25;
        public int RedrawGoldIncrease = 5;
        public int FreeDrawCooldownInSeconds = 15;
        public Button DrawButton;
        public TextMeshProUGUI DrawCooldownText;
        public GameObject RedrawCostIndicator;
        public TextMeshProUGUI RedrawCostText;
        public Image DrawCooldownIcon;
        private float _freeDrawCooldown;

        [Header("Player Energy")]
        [Space(20)]
        //private int _remainingEnergy;
        //private int _startingEnergy = 9; //ends up as _startingEnergy + 1 because we get one "tick" of income at the start
        //public int EnergyGainedPerTurn = 1;
        //public int MaxEnergy = 10;
        
        [Header("Delayed Effects")]
        [Space(20)]
        public List<Effect> TemporaryOnTurnStartEffects = new List<Effect>();
        public List<Effect> PermanentOnTurnStartEffects = new List<Effect>();
        // @formatter:on 
        /*public int RemainingEnergy
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
        }*/

        // @formatter:off
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
            // ModifyEnergy(_startingEnergy);
        }

        private void Start()
        {
            StartTurn();
            DrawNewHand();
            _freeDrawCooldown = 0f;
        }

        public void Update()
        {
            UpdateDrawTimer();
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }
        }

        private void UpdateDrawTimer()
        {
            _freeDrawCooldown -= Time.deltaTime;
            DrawCooldownText.text =
                _freeDrawCooldown <= 0f ? "" : _freeDrawCooldown.ToString("00");
            DrawButton.interactable = _freeDrawCooldown <= 0f || IncomeController.GetCurrentGold() >= RedrawGoldCost;
            RedrawCostText.text     = RedrawGoldCost.ToString();
            RedrawCostIndicator.SetActive(_freeDrawCooldown >= 0.0f);
            DrawCooldownIcon.fillAmount = _freeDrawCooldown / FreeDrawCooldownInSeconds;
        }

        public void DrawNewHand()
        {
            if (_freeDrawCooldown <= 0f)
            {
                PlayerHand.DiscardHand();
                Instance.Draw(Instance.CardsToDraw);
            }
            else if (IncomeController.GetCurrentGold() >= RedrawGoldCost)
            {
                PlayerHand.DiscardHand();
                Instance.Draw(Instance.CardsToDraw);
                IncomeController.ModifyGold(-RedrawGoldCost);
                RedrawGoldCost += RedrawGoldIncrease;
            }

            _freeDrawCooldown = FreeDrawCooldownInSeconds;
        }

        public static void StartTurn()
        {
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

        public static void RegisterTemporaryOnTurnEffect(Effect toRegister)
        {
            Instance.TemporaryOnTurnStartEffects.Add(toRegister);
        }

        public static void RegisterPermanentOnTurnEffect(Effect toRegister)
        {
            Instance.PermanentOnTurnStartEffects.Add(toRegister);
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