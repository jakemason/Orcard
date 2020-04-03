using UnityEngine;

public class ConstructionIndicator : MonoBehaviour
{
    public static ConstructionIndicator Instance;
    public GameObject Indicator;
    public DrawCircle RangeDrawer;
    public LineRenderer Line;

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public static void Enable(BuildingCard model)
    {
        Instance.Indicator.SetActive(true);
        TowerCard tower = model as TowerCard;
        if (tower != null)
        {
            Instance.RangeDrawer.Xradius = tower.Range;
            Instance.RangeDrawer.Yradius = tower.Range;
        }

        Instance.RangeDrawer.Change();
        Instance.Line.enabled = true;
    }

    public static void Disable()
    {
        Instance.Line.enabled = false;
        Instance.Indicator.SetActive(false);
    }
}