using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("Physics 2D/Ellipse Collider 2D")]

[RequireComponent(typeof(EdgeCollider2D))]
public class EllipseCollider2D : MonoBehaviour {

    [Range(1, 25)]
    public float radiusX = 1, radiusY = 2;

    [Range(10,90)]
    public int smoothness = 30;

    [Range(0, 180)]
    public int rotation = 0;
    
    Vector2 origin, center;
    
    public Vector2[] getPoints(Vector2 off)
    {
        List<Vector2> points = new List<Vector2>();

        origin = transform.localPosition;
        center = origin + off;
        
        float ang = 0;
        float o = rotation * Mathf.Deg2Rad;

        for (int i = 0; i <= smoothness; i++)
        {
            float a = ang * Mathf.Deg2Rad;
            float x = center.x + radiusX * Mathf.Cos(a) * Mathf.Cos(o) - radiusY * Mathf.Sin(a) * Mathf.Sin(o);
            float y = center.y - radiusX * Mathf.Cos(a) * Mathf.Sin(o) - radiusY * Mathf.Sin(a) * Mathf.Cos(o);

            points.Add(new Vector2(x, y));
            ang += 360f/smoothness;
        }

        return points.ToArray();
    }
}