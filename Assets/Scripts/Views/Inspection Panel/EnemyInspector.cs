﻿using UnityEngine;

public class EnemyInspector : MonoBehaviour, IInspectable
{
    public EnemyController EnemyReference;
    private Enemy _model;

    private void Start()
    {
        _model = EnemyReference.Model;
    }

    public void EnableInspection()
    {
        InspectorManager.Instance.Name.text = "<b>" + _model.Name + "</b>";

        string details = "<b>HP</b>: " + _model.HP + "\n";
        details += "<b>Speed</b>: " + _model.Speed + "\n";
        details += "<i>" + _model.FlavorText       + "</i>\n";

        InspectorManager.Instance.Details.text = details;
        InspectorManager.Enable();
    }

    public void OnMouseOver()
    {
        EnableInspection();
    }
}