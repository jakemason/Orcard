using TMPro;
using UnityEngine;

public class IncomeController : MonoBehaviour
{
    private static IncomeController _instance;
    public int SecondsBetweenIncomeTicks = 15;
    private float _tickCountdown;
    public int CurrentGold;
    public int Income = 25;
    public TextMeshProUGUI GoldText;

    private void Start()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(this);
        }

        _tickCountdown          = SecondsBetweenIncomeTicks;
        _instance.GoldText.text = CurrentGold.ToString();
    }


    private void Update()
    {
        _tickCountdown -= Time.deltaTime;
        if (_tickCountdown <= 0f)
        {
            ModifyGold(Income);
            _tickCountdown = SecondsBetweenIncomeTicks;
        }
    }

    public static int GetCurrentGold()
    {
        return _instance.CurrentGold;
    }

    public static void ModifyGold(int modifier)
    {
        _instance.CurrentGold += modifier;
        if (_instance.CurrentGold < 0)
        {
            _instance.CurrentGold = 0;
        }

        _instance.GoldText.text = _instance.CurrentGold.ToString();
    }
}