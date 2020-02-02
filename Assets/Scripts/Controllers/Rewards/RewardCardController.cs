using Players;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CardRenderer))]
public class RewardCardController : MonoBehaviour, IPointerDownHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        Player.Instance.DeckForCurrentRun.Cards.Add(GetComponent<CardRenderer>().CardObject);
        RewardsManager.CloseRewardsPanel();
    }
}