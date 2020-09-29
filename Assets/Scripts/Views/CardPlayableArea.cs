using UnityEngine;
using UnityEngine.EventSystems;

public class CardPlayableArea : MonoBehaviour
{
    private Camera _camera;
    private static CardPlayableArea _instance;


    private void Start()
    {
        if (_instance == null)
        {
            _instance = this;
            _camera   = Camera.main;
        }
        else
        {
            Destroy(this);
        }
    }

    public static void PlayCard(PointerEventData eventData)
    {
        PlayableCardController c = eventData.pointerDrag.GetComponent<PlayableCardController>();
        if (!c) return;

        SpellCast.AttemptingToCast = c.CardObject;
        SpellCast.LastCardPlayed   = c;
        SpellCast.CastPosition     = _instance._camera.ScreenPointToRay(eventData.position).origin;

        SpellCast.CastPosition = new Vector2(
            Mathf.RoundToInt(SpellCast.CastPosition.x),
            Mathf.RoundToInt(SpellCast.CastPosition.y));

        SpellCast.Target = BuildingManager.GetBuildingAt(SpellCast.CastPosition);

        if (!SpellCast.CastingRequirementsMet()) return;
        PlayerHand.DiscardCard(c);
        SpellCast.Resolve();
    }

    /*public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Attempting to cast a drop");
    }*/
}