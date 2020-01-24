using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CardRenderer))]
public class PlayableCardController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDragHandler,
    IBeginDragHandler, IEndDragHandler
{
    // @formatter:off 
    [Header("Card Movement")]
    public Card CardObject;
    public bool MovementDisabled = false;
    public Vector3 TargetPosition = Vector3.negativeInfinity;
    public Vector3 TargetRotation = Vector3.negativeInfinity;
    public Vector3 TargetScale = new Vector3(1, 1, 1);
    private bool _isDragging;

    [Tooltip("How quickly cards change their position, rotation, and scale.")]
    public float MovementSpeed = 5.0f;

    [Tooltip("How quickly cards react once marked for death. We go to the discard pile quicker than to hand.")]
    public float DestroyMovementSpeed = 2.0f;

    [Tooltip("Denotes when a card is animating into the discard pile and is about to be removed from the object hierarchy.")]
    public bool MarkedForDestruction = false;

    [Header("Hover State")] 
    public Vector3 HoverOffset = new Vector3(0, 80f, 0);
    public Vector3 RestingPosition;
    public Vector3 RestingRotation;
    public int RestingSiblingIndex;
    public float HoverScale = 1.5f;
    
    private RectTransform _trans;
    // @formatter:on 

    private void Start()
    {
        _trans = GetComponent<RectTransform>();
        CardRenderer cardRenderer = GetComponent<CardRenderer>();
        CardObject = cardRenderer.CardObject;
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
            //if we're marked for Destruction and at our Target scale of 0,0,0 as we shrink to the discard pile,
            //we can destroy ourselves
            Destroy(gameObject);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (MarkedForDestruction || _isDragging) return;
        TargetPosition      = RestingPosition + HoverOffset;
        TargetRotation      = Vector3.zero;
        RestingSiblingIndex = transform.GetSiblingIndex();
        TargetScale         = new Vector3(HoverScale, HoverScale, HoverScale);
        transform.SetSiblingIndex(99);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (MarkedForDestruction || _isDragging) return;
        TargetPosition = RestingPosition;
        TargetRotation = RestingRotation;
        TargetScale    = Vector3.one;
        transform.SetSiblingIndex(RestingSiblingIndex);
    }

    public void OnDrag(PointerEventData eventData)
    {
        MovementDisabled   = true;
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _isDragging      = false;
        MovementDisabled = false;
        TargetPosition   = RestingPosition;
        TargetRotation   = RestingRotation;
        TargetScale      = Vector3.one;
        transform.SetSiblingIndex(RestingSiblingIndex);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _isDragging = true;
    }
}