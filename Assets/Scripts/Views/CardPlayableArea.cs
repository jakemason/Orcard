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
        SpellCast.LastCardPlayed   = c;
        SpellCast.CastPosition     = _camera.ScreenPointToRay(eventData.position).origin;

        SpellCast.CastPosition = new Vector2(
            Mathf.RoundToInt(SpellCast.CastPosition.x),
            Mathf.RoundToInt(SpellCast.CastPosition.y));

        SpellCast.Target = BuildingManager.GetBuildingAt(SpellCast.CastPosition);

        if (!SpellCast.CastingRequirementsMet()) return;
        PlayerHand.DiscardCard(c);
        SpellCast.Resolve();
    }
}