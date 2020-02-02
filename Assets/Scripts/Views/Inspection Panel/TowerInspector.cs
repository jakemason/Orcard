using UnityEngine;

public class TowerInspector : MonoBehaviour, IInspectable
{
    public Tower TowerReference;
    private TowerCard _model;

    private void Start()
    {
        _model = TowerReference.Model;
    }

    public void EnableInspection()
    {
        InspectorManager.Instance.Sprite.sprite = _model.Artwork;
        InspectorManager.Instance.Name.text     = "<b>" + _model.Name + "</b>";

        string details = "";
        details                                += "<b>Damage</b>: " + _model.Damage                         + "\n";
        details                                += "<b>Attack Rate</b>: " + _model.AttackRate.ToString("n2") + "\n";
        details                                += "<b>DPS (Est.)</b>: " + _model.DamagePerSecond            + "\n";
        details                                += _model.InstructionText;
        InspectorManager.Instance.Details.text =  details;
        InspectorManager.Enable();
    }

    public void OnMouseOver()
    {
        EnableInspection();
    }
}