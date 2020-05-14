using Players;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(CardRenderer))]
public class PlayableCardController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDragHandler,
    IBeginDragHandler, IEndDragHandler, ITargetable
{
    // @formatter:off 
    [Header("Card Movement")]
    public Card CardObject;
    [ReadOnly] public Card OriginalCard;
    public bool MovementDisabled;
    public Vector3 TargetPosition = Vector3.negativeInfinity;
    public Vector3 TargetRotation = Vector3.negativeInfinity;
    public Vector3 TargetScale = new Vector3(1, 1, 1);
    public ParticleSystem ParticleSystem;
    
    //this needs to be static because we don't want to ever drag more than one card at a time.
    //if this _isn't_ static we get weird SiblingIndex issues when we don't actually finish a cast.
    private static bool _isDragging;

    [Tooltip("How quickly cards change their position, rotation, and scale.")]
    public float MovementSpeed = 5.0f;

    [Tooltip("How quickly cards react once marked for death. We go to the discard pile quicker than to hand.")]
    public float DestroyMovementSpeed = 12.0f;

    [Tooltip("Denotes when a card is animating into the discard pile and is about to be removed from the object hierarchy.")]
    public bool MarkedForDestruction = false;

    [Header("Hover State")] 
    public Vector3 HoverOffset = new Vector3(0, 80f, 0);
    public Vector3 RestingPosition;
    public Vector3 RestingRotation;
    public int RestingSiblingIndex;
    public float HoverScale = 1.5f;
    public Image HoverCollider;
    
    private RectTransform _trans;
    // @formatter:on

    private void Start()
    {
        _trans = GetComponent<RectTransform>();
        CardRenderer cardRenderer = GetComponent<CardRenderer>();
        CardObject = cardRenderer.CardObject;
        ParticleSystem.Stop();

        //NOTE: 
        //Here we keep an original copy of the card so that we can create effects that modify the card,
        //but do not persist through turns. For example, you might reduce the cost of a random card in your hand
        //FOR THIS TURN but you don't want it to cost 0 the next time you draw that card. We achieve this by not
        //"discarding" the card we're playing, but instead we discard the OriginalCard so that we can draw a fresh card
        //the next time.
        //
        //If we DO want permanent effects (for this run) we just target the OriginalCard as well as the CardObject.
        OriginalCard = Instantiate(cardRenderer.CardObject);
    }

    private void Update()
    {
        float movementSpeed = MovementSpeed;
        if (MarkedForDestruction)
        {
            movementSpeed = DestroyMovementSpeed;
        }

        if (!MovementDisabled)
        {
            ReturnToRest(movementSpeed);
        }
    }

    /// <summary>
    /// If the card is not currently being moved around, return the card to its resting position in the hand.
    /// </summary>
    /// <param name="movementSpeed">The speed at which the card is returned to its resting position.</param>
    private void ReturnToRest(float movementSpeed)
    {
        Vector3    pos           = _trans.anchoredPosition;
        Quaternion transRotation = _trans.localRotation;
        Vector3    scale         = _trans.localScale;
        Vector3    rot           = transRotation.eulerAngles;

        if (Vector3.Distance(pos, TargetPosition) > 0.01f)
        {
            _trans.anchoredPosition = Vector3.Lerp(pos, TargetPosition, movementSpeed * Time.deltaTime);
        }

        if (Vector3.Distance(rot, TargetRotation) > 0.01f)
        {
            rot.z = Mathf.LerpAngle(transRotation.eulerAngles.z, TargetRotation.z,
                movementSpeed * Time.deltaTime);
            _trans.eulerAngles = rot;
        }

        if (Vector3.Distance(scale, TargetScale) > 0.01f)
        {
            _trans.localScale = Vector3.Lerp(scale, TargetScale, movementSpeed * Time.deltaTime);
        }
        else if (MarkedForDestruction)
        {
            Destroy(gameObject);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (MarkedForDestruction || _isDragging) return;
        if (IncomeController.GetCurrentGold() >= CardObject.CastingCost)
        {
            ParticleSystem.Play();
        }

        TargetPosition      = RestingPosition + HoverOffset;
        TargetRotation      = Vector3.zero;
        RestingSiblingIndex = transform.GetSiblingIndex();
        TargetScale         = new Vector3(HoverScale, HoverScale, HoverScale);
        transform.SetSiblingIndex(99);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (MarkedForDestruction || _isDragging) return;
        ParticleSystem.Stop();
        TargetPosition = RestingPosition;
        TargetRotation = RestingRotation;
        TargetScale    = Vector3.one;
        transform.SetSiblingIndex(RestingSiblingIndex);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (IncomeController.GetCurrentGold() < CardObject.CastingCost)
        {
            return;
        }

        ParticleSystem.Play();
        MovementDisabled   = true;
        transform.position = Input.mousePosition;

        BuildingCard buildingCard = CardObject as BuildingCard;

        if (buildingCard || CardObject.IsTargeted)
        {
            TargetIndicator.Enable(buildingCard);
            TargetScale          = Vector3.zero;
            transform.localScale = Vector3.zero;
        }

        HoverCollider.raycastTarget = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _isDragging      = false;
        MovementDisabled = false;
        ParticleSystem.Stop();


        if (MarkedForDestruction) return;
        TargetPosition = RestingPosition;
        TargetRotation = RestingRotation;
        TargetScale    = Vector3.one;
        transform.SetSiblingIndex(RestingSiblingIndex);
        TargetIndicator.Disable();
        HoverCollider.raycastTarget = true;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (IncomeController.GetCurrentGold() < CardObject.CastingCost)
        {
            return;
        }

        _isDragging = true;
    }
}