using System.Collections.Generic;
using UnityEngine;

public class Tower : Building, ITargetable
{
    // @formatter:off 
    public new TowerCard Model;
    public List<GameObject> EnemiesInRange;

    private float _attackCooldown = 0f;
    private GameObject _currentTarget = null;
    public DrawCircle RangeIndicator;
    public int MaxAmmo;
    private GameObject _reloadAnimation;
    private Animator _animationController;
    public int RemainingAmmo;
    private static readonly int IsFiring = Animator.StringToHash("isFiring");

    // @formatter:on 
    private void Start()
    {
        EnemiesInRange       = new List<GameObject>();
        RangeIndicator       = GetComponentInChildren<DrawCircle>();
        _animationController = GetComponent<Animator>();
        MaxAmmo              = Model.MaxAmmo;
        RemainingAmmo        = MaxAmmo;
        SetRange();
    }

    private void Update()
    {
        if (_currentTarget == null && EnemiesInRange.Count > 0)
        {
            _currentTarget = EnemiesInRange[0];
        }

        _attackCooldown -= Time.deltaTime;
        Fire();
    }

    private void Fire()
    {
        if ((RemainingAmmo <= 0 || Model.Damage == 0 || !_currentTarget || !(_attackCooldown <= 0)))
        {
            _animationController.SetBool(IsFiring, false);
            return;
        }

        _animationController.SetBool(IsFiring, true);

        Vector3 boltOffset     = new Vector3(0.0f, 0.5f, 0.0f);
        Vector3 targetPosition = _currentTarget.transform.position;
        Vector3 position       = transform.position + boltOffset;
        Vector3 vectorToTarget = targetPosition     - position;
        float   angle          = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;

        _attackCooldown = Model.AttackRate;
        GameObject bolt =
            Instantiate(Model.BoltPrefab, position + boltOffset,
                Quaternion.Euler(new Vector3(0, 0, angle)));
        TowerAttack tbolt = bolt.GetComponent<TowerAttack>();
        tbolt.Target        = targetPosition;
        tbolt.MovementSpeed = Model.AttackMovementSpeed;
        tbolt.Damage        = Model.Damage;
        //RemainingAmmo--; //TODO: !=============== Uncomment to re-enable ammo functionality =====================!
        if (RemainingAmmo <= 0)
        {
            _reloadAnimation = Instantiate(BuildingManager.Instance.ReloadAnimation, transform.position,
                Quaternion.identity);
        }
    }

    public void Reload()
    {
        RemainingAmmo = MaxAmmo;
        Destroy(_reloadAnimation);
    }

    private void SetRange()
    {
        if (RangeIndicator)
        {
            RangeIndicator.Xradius = Model.Range;
            RangeIndicator.Yradius = Model.Range;
            RangeIndicator.CreatePoints();
        }


        CircleCollider2D circleCollider2D = GetComponent<CircleCollider2D>();
        if (circleCollider2D)
        {
            circleCollider2D.radius = Model.Range;
        }
    }

    public new void DoStartOfTurnEffects()
    {
        foreach (Effect effect in Model.EachTurnEffects)
        {
            effect.Activate();
        }
    }

    //TODO: DO a layer check and setup the layer matrix appropriately, probably needed for mobile perf
    public void OnTriggerEnter2D(Collider2D col)
    {
        EnemiesInRange.Add(col.gameObject);
    }

    public void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject == _currentTarget)
        {
            _currentTarget = null;
        }

        EnemiesInRange.Remove(col.gameObject);
    }
}