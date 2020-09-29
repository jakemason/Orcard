using TMPro;
using UnityEngine;

public class IncomeController : MonoBehaviour
{
    private static IncomeController _instance;
    public int SecondsBetweenIncomeTicks = 15;
    private float _tickCountdown;
    public int CurrentGold;
    public int Income = 25;
    public int GoldMultiplier = 1;
    public TextMeshProUGUI GoldText;
    public AudioClip ToPlay;
    public AudioSource AudioSource;

    private void Awake()
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
        SetGold(Income);
    }

    public static void SetMultiplier(int toSet)
    {
        _instance.GoldMultiplier = toSet;
    }

    public static int GetCurrentGold()
    {
        return _instance.CurrentGold;
    }

    public static void SetGold(int toSet)
    {
        _instance.CurrentGold   = toSet;
        _instance.GoldText.text = _instance.CurrentGold.ToString();
    }

    public static void ModifyGold(int modifier)
    {
        if (modifier > 0)
        {
            _instance.CurrentGold       += modifier * _instance.GoldMultiplier;
            _instance.GoldMultiplier    =  1;
            _instance.AudioSource.pitch =  Random.Range(0.8f, 1.2f);
            _instance.AudioSource.PlayOneShot(_instance.ToPlay);
        }
        else
        {
            _instance.CurrentGold += modifier;
        }

        if (_instance.CurrentGold < 0)
        {
            _instance.CurrentGold = 0;
        }

        _instance.GoldText.text = _instance.CurrentGold.ToString();
    }
}