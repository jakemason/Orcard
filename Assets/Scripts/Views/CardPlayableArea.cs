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
        Debug.Log(SpellCast.CastPosition);
        SpellCast.CastPosition = new Vector2(
            Mathf.RoundToInt(SpellCast.CastPosition.x),
            Mathf.RoundToInt(SpellCast.CastPosition.y));
        Debug.Log(SpellCast.CastPosition);
        SpellCast.Target = TowerManager.Instance.ConstructedTowers.ContainsKey(SpellCast.CastPosition)
            ? TowerManager.Instance.ConstructedTowers[SpellCast.CastPosition]
            : null;
        Debug.Log(SpellCast.Target);
        if (!SpellCast.CastingRequirementsMet()) return;
        PlayerHand.DiscardCard(c);
        SpellCast.Resolve();
    }
}