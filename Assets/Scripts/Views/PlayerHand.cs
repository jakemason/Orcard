using System.Collections.Generic;
using Players;
using UnityEngine;

public class PlayerHand : MonoBehaviour
{
    // @formatter:off
    public List<GameObject> HeldCards;
    public static PlayerHand Instance;
    
    [Header("Held Card Positioning")]
    public float HorizontalOffset = 85f;
    public float VerticalOffset = -7.5f;
    public float RotationOffset = -3.5f;
    
    public RectTransform DiscardPile;
    // @formatter:on

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddCardToHand(GameObject card)
    {
        HeldCards.Add(card);
        AlignCards();
    }

    /// <summary>
    /// Returns the number of cards remaining in the Player's Hand
    /// </summary>
    /// <returns>The number of cards in the Player's Hand</returns>
    public int CardsInHand()
    {
        return HeldCards.Count;
    }

    /// <summary>
    /// Discards a card from the player's hand.
    /// </summary>
    /// <param name="cardToDiscard">The card to discard.</param>
    public static void DiscardCard(PlayableCardController cardToDiscard)
    {
        GameObject go = cardToDiscard.gameObject;

        Instance.HeldCards.Remove(go);
        cardToDiscard.TargetRotation       = Vector3.zero;
        cardToDiscard.TargetScale          = Vector3.zero;
        cardToDiscard.MarkedForDestruction = true;
        cardToDiscard.TargetPosition       = Instance.DiscardPile.anchoredPosition;

        Card card = cardToDiscard.CardObject;
        if (!card.DestroyOnCast)
        {
            Player.Instance.DiscardPile.Add(cardToDiscard.OriginalCard);
        }

        SpellCard spellCard = card as SpellCard;
        if (spellCard != null)
        {
            foreach (Effect discardEffect in spellCard.DiscardEffects)
            {
                discardEffect.Activate();
            }
        }

        Instance.AlignCards();
    }

    /// <summary>
    /// Discards the entire hand of cards.
    /// </summary>
    public static void DiscardHand()
    {
        for (int i = 0; i < Instance.HeldCards.Count; i++)
        {
            PlayableCardController rend = Instance.HeldCards[i].GetComponent<PlayableCardController>();
            rend.TargetRotation       = Vector3.zero;
            rend.TargetScale          = Vector3.zero;
            rend.TargetPosition       = Instance.DiscardPile.anchoredPosition;
            rend.MarkedForDestruction = true;
            Card card = rend.CardObject;
            Player.Instance.DiscardPile.Add(card);
        }

        Instance.HeldCards = new List<GameObject>();
    }

    /// <summary>
    /// Aligns and rotates all the cards in the player's hand.
    /// </summary>
    private void AlignCards()
    {
        int cardsInHand = CardsInHand();
        if (cardsInHand == 0)
        {
            return;
        }

        int                    midPoint = cardsInHand / 2;
        GameObject             mid;
        PlayableCardController cardRenderer;
        if (cardsInHand == 1)
        {
            mid                          = HeldCards[0];
            cardRenderer                 = mid.GetComponent<PlayableCardController>();
            cardRenderer.TargetRotation  = Vector3.zero;
            cardRenderer.TargetPosition  = Vector3.zero;
            cardRenderer.RestingPosition = cardRenderer.TargetPosition;
            cardRenderer.RestingRotation = cardRenderer.TargetRotation;
            return;
        }

        mid                          = HeldCards[midPoint];
        cardRenderer                 = mid.GetComponent<PlayableCardController>();
        cardRenderer.TargetRotation  = Vector3.zero;
        cardRenderer.TargetPosition  = Vector3.zero;
        cardRenderer.RestingPosition = cardRenderer.TargetPosition;
        cardRenderer.RestingRotation = cardRenderer.TargetRotation;

        //align all our cards to the left of the middle card
        int index = 1;
        for (int i = midPoint - 1; i >= 0; i--)
        {
            GameObject go = HeldCards[i];
            cardRenderer = go.GetComponent<PlayableCardController>();
            float multiplier       = -1.0f;
            float rotationOffset   = (index) * RotationOffset   * multiplier;
            float horizontalOffset = (index) * HorizontalOffset * multiplier;
            float verticalOffset   = (index)                    * VerticalOffset;

            cardRenderer.TargetRotation  = new Vector3(0f,               0f,             rotationOffset);
            cardRenderer.TargetPosition  = new Vector3(horizontalOffset, verticalOffset, 0f);
            cardRenderer.RestingPosition = cardRenderer.TargetPosition;
            cardRenderer.RestingRotation = cardRenderer.TargetRotation;
            index++;
        }

        //align all our cards that are beyond the middle card
        for (int i = midPoint + 1; i < HeldCards.Count; i++)
        {
            GameObject go = HeldCards[i];
            cardRenderer = go.GetComponent<PlayableCardController>();
            float multiplier       = 1.0f;
            float rotationOffset   = (i - midPoint) * RotationOffset   * multiplier;
            float horizontalOffset = (i - midPoint) * HorizontalOffset * multiplier;
            float verticalOffset   = (i - midPoint)                    * VerticalOffset;

            cardRenderer.TargetRotation  = new Vector3(0f,               0f,             rotationOffset);
            cardRenderer.TargetPosition  = new Vector3(horizontalOffset, verticalOffset, 0f);
            cardRenderer.RestingPosition = cardRenderer.TargetPosition;
            cardRenderer.RestingRotation = cardRenderer.TargetRotation;
        }
    }
}