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
            PlayerController.Instance.DeckForCurrentRun.Cards.Add(GetComponent<CardRenderer>().CardObject);
            IncomeController.ModifyGold(-_card.GoldCost);
            PlayOneShotSound.Play(PurchaseSound, 1.5f, 2.0f);
            Destroy(gameObject);
        }
    }
}