using System;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(LineRenderer))]
public class DrawCircle : MonoBehaviour
{
    [Range(0, 50)] public int Segments = 50;
    [Range(0, 50)] public float Xradius = 5;
    [Range(0, 50)] public float Yradius = 5;

    [FormerlySerializedAs("_line")] public LineRenderer Line;

    private void Start()
    {
        Line = gameObject.GetComponent<LineRenderer>();

        Line.positionCount = (Segments + 1);
        Line.useWorldSpace = false;
    }

    public void CreatePoints()
    {
        float x;
        float y;

        float change = 2 * (float) Math.PI / Segments;
        float angle  = change;
        Line.positionCount = Segments + 1;

        for (int i = 0; i < (Segments + 1); i++)
        {
            x = Mathf.Sin(angle) * Xradius;
            y = Mathf.Cos(angle) * Yradius;

            Line.SetPosition(i, new Vector3(x, y, 0));

            angle += change;
        }
    }

    private void OnValidate()
    {
        CreatePoints();
    }
}