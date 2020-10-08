using System.Diagnostics;
using Players;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CardRenderer))]
public class ShopCardController : MonoBehaviour, IPointerDownHandler
{
    public TextMeshProUGUI Cost;
    public AudioClip PurchaseSound;
    public bool DestroyOnPurchase = true;
    private Card _card;

    public void Start()
    {
        _card     = GetComponent<CardRenderer>().CardObject;
        Cost.text = _card.GoldCost.ToString();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (IncomeController.GetCurrentGold() >= _card.GoldCost)
        {
            CardRenderer rend = GetComponent<CardRenderer>();
            PlayerController.Instance.DeckForCurrentRun.Cards.Add(rend.CardObject);
            IncomeController.ModifyGold(-_card.GoldCost);
            PlayOneShotSound.Play(PurchaseSound, 1.5f, 2.0f);
            if (rend.CardObject.DoNotRemoveFromShop)
            {
                return;
            }

            Destroy(gameObject);
        }
        else
        {
            PlayOneShotSound.Play(SoundLibrary.Global.ErrorSound, 0.9f, 1.0f);
            ClosedCaptioning.CreateMessage("You can't afford this purchase");
        }
    }
}