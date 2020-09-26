using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    public float ScrollSpeed;
    public float FadeTime;

    private float _startFadeTime;
    private SpriteRenderer _sprite;
    private TextMeshPro _text;

    private void Awake()
    {
        _text          = GetComponent<TextMeshPro>();
        _startFadeTime = FadeTime;
    }

    public void Update()
    {
        transform.position -= Vector3.up * (Time.deltaTime * ScrollSpeed);
        FadeTime           -= Time.deltaTime;
        Color color = _text.color;
        color.a     = FadeTime / _startFadeTime;
        _text.color = color;
        if (FadeTime <= 0.0f)
        {
            Destroy(gameObject);
        }
    }

    public static void CreateCombatText(GameObject combatTextPrefab, Vector3 pos, int damageDealt,
        bool wasCrit = false)
    {
        Vector3    spawnOffset = new Vector3(0, 2, 0);
        GameObject combatText  = Instantiate(combatTextPrefab, pos + spawnOffset, Quaternion.identity);
        combatText.name = "Combat Text";
        TextMeshPro text = combatText.GetComponent<TextMeshPro>();
        text.text = "-" + damageDealt;
    }
}