using System;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class DrawCircle : MonoBehaviour
{
    [Range(0, 50)] public int Segments = 50;
    [Range(0, 50)] public float Xradius = 5;
    [Range(0, 50)] public float Yradius = 5;

    public LineRenderer Line;

    private void Awake()
    {
        Line.positionCount = (Segments + 1);
        Line.useWorldSpace = false;
        Line.startWidth    = 0.1f;
        Line.endWidth      = 0.1f;
        CreatePoints();
    }

    public void Change()
    {
        CreatePoints();
    }

    public void CreatePoints()
    {
        if (!Line) return;

        float x;
        float y;

        float change = 2 * (float) Math.PI / Segments;
        float angle  = change;
        Line.positionCount = Segments + 1;

        for (int i = 0; i < (Segments + 1); i++)
        {
            x = Mathf.Sin(angle) * Xradius;
            y = Mathf.Cos(angle) * Yradius;

            Line.SetPosition(i, new Vector3(x, y, -1));

            angle += change;
        }
    }

    private void OnValidate()
    {
        Change();
    }

    public void OnMouseOver()
    {
        if (RewardsManager.IsOpen) return;
        Line.enabled = true;
    }

    public void OnMouseExit()
    {
        Line.enabled = false;
    }
}