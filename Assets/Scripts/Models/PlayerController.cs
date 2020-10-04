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
        public AudioClip DrawHandSound;
        public AudioClip TowerAttackSound;
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
            TurnManager.StartTurn();
            _freeDrawCooldown = 0f;
        }

        public void Update()
        {
            UpdateDrawTimer();
            //TODO: Hardcoded KeyCode
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (_freeDrawCooldown <= 0f)
                {
                    DrawNewHand();
                }
            }
        }

        private void UpdateDrawTimer()
        {
            _freeDrawCooldown -= Time.deltaTime;
            DrawCooldownText.text =
                _freeDrawCooldown <= 0f ? "" : _freeDrawCooldown.ToString("00");
            DrawButton.interactable = _freeDrawCooldown <= 0f; //|| IncomeController.GetCurrentGold() >= RedrawGoldCost;

            // Uncommenting the following two lines will enable a number  representation of the cooldown, but I think
            // it's likely unnecessary. 

            //RedrawCostText.text     = RedrawGoldCost.ToString();
            //RedrawCostIndicator.SetActive(_freeDrawCooldown >= 0.0f);
            DrawCooldownIcon.fillAmount = _freeDrawCooldown / FreeDrawCooldownInSeconds;
        }

        public void DrawNewHand()
        {
            if (_freeDrawCooldown <= 0f)
            {
                PlayerHand.DiscardHand();
                ShopView.Disable();
                Instance.Draw(Instance.CardsToDraw);
                PlayOneShotSound.Play(Instance.DrawHandSound, 0.8f, 1.2f);
                IncomeController.SetGold(0);
#if UNITY_EDITOR
                IncomeController.SetGold(500);
#endif
                TurnManager.StartTurn();
            }
            // This was back when we allowed paying gold to redraw whenever we like. This was probably a design
            // balance nightmare to work with, so we cut it instead.
            /*else if (IncomeController.GetCurrentGold() >= RedrawGoldCost)
            {
                PlayerHand.DiscardHand();
                Instance.Draw(Instance.CardsToDraw);
                IncomeController.ModifyGold(-RedrawGoldCost);
                RedrawGoldCost += RedrawGoldIncrease;
            }*/

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

            // Now that we've drawn our hand, we trigger our OnDraw effects. We only do this once ALL cards have been
            // drawn because some draw effects might target cards in hand and we want them to affect all the cards
            // in our hand or, at the very least, have the ability to do so
            foreach (Card card in PlayerHand.Instance.GetHeldCardsData())
            {
                Debug.Log($"Checking {card} for OnDraw effects.");
                /*
                 * TODO: We need some sort of animation queue here to better demonstrate what is happening each Draw.
                 *
                 * For example, if I draw a card that has a "Discard a random card" OnDraw effect...it will simply
                 * look like I drew 1 fewer card than I expected because I don't _see_ the discard happen. We also
                 * don't get a chance to see what the card we're discarding is. This is also true of cards that ADD
                 * cards to our deck -- we don't get to _see_ what we actually added.
                 */
                foreach (Effect drawEffect in card.OnDrawEffects)
                {
                    Debug.Log($"Activating Draw Effect [{drawEffect.name}] for card [{card.Name}].");
                    drawEffect.Activate();
                }
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

            GameObject drawnCard = Instantiate(CardRendererTemplate, HandGameObject.transform);
            drawnCard.transform.position = DeckPosition.position;
            PlayableCardController controller = drawnCard.GetComponent<PlayableCardController>();
            controller.CardObject   = card;
            controller.OriginalCard = card;
            CardRenderer drawnCardRenderer = drawnCard.GetComponent<CardRenderer>();
            drawnCardRenderer.CardObject = card;
            drawnCardRenderer.UpdateCardDetails();
            PlayerHand.Instance.AddCardToHand(drawnCard);
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