using UnityEngine;

public class TowerPlacementController : MonoBehaviour
{
    public GameObject GhostIndicator;
    public GameObject TowerToPlace;
    private bool _isPlacingTower;
    private Camera _battleCamera;
    public static bool Disabled;

    private void Start()
    {
        _battleCamera = Camera.main;
    }

    private void Update()
    {
        if (Disabled) return;

        if (_isPlacingTower)
        {
            MoveGhost();
            if (Input.GetButtonUp("Fire1"))
            {
                GhostIndicator.SetActive(false);
                _isPlacingTower = false;
                Vector2 newPos = _battleCamera.ScreenToWorldPoint(Input.mousePosition);
                Instantiate(TowerToPlace, newPos, Quaternion.identity);
                PlayerResources.ModifyPlayerEnergy(-1);
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1") && PlayerResources.HasEnergy())
            {
                GhostIndicator.SetActive(true);
                _isPlacingTower = true;
            }
        }
    }

    private void MoveGhost()
    {
        Vector2 newPos = _battleCamera.ScreenToWorldPoint(Input.mousePosition);
        GhostIndicator.transform.position = newPos;
    }
}