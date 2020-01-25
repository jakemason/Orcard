using UnityEngine;
using UnityEngine.EventSystems;

public class CardPlayableArea : MonoBehaviour, IDropHandler
{
    private Camera _camera;

    private void Start()
    {
        _camera = Camera.main;
    }

    public void OnDrop(PointerEventData eventData)
    {
        PlayableCardController c = eventData.pointerDrag.GetComponent<PlayableCardController>();
        if (!c) return;

        SpellCast.AttemptingToCast = c.CardObject;
        SpellCast.CastPosition     = _camera.ScreenPointToRay(eventData.position).origin;
        if (!SpellCast.CastingRequirementsMet()) return;
        PlayerHand.DiscardCard(c);
        SpellCast.Resolve();
    }
}