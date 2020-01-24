using UnityEngine;

public class PlayerResources : MonoBehaviour
{
    public static PlayerResources Instance;
    public int MaxEnergy = 3;
    public int RemainingEnergy = 3;

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

        RemainingEnergy = MaxEnergy;
    }

    public static bool HasEnergy()
    {
        return Instance.RemainingEnergy > 0;
    }

    public static void ModifyPlayerEnergy(int modifier)
    {
        Instance.RemainingEnergy += modifier;
    }

    public static void NextTurn()
    {
        RefillEnergy();
    }

    private static void RefillEnergy()
    {
        Instance.RemainingEnergy = Instance.MaxEnergy;
        Debug.Log(Instance.RemainingEnergy);
    }
}