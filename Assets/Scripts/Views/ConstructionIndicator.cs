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

    public static void Enable(TowerCard model)
    {
        Instance.Indicator.SetActive(true);
        Instance.RangeDrawer.Xradius = model.Range;
        Instance.RangeDrawer.Yradius = model.Range;
        Instance.RangeDrawer.Change();
        Instance.Line.enabled = true;
    }

    public static void Disable()
    {
        Instance.Line.enabled = false;
        Instance.Indicator.SetActive(false);
    }
}