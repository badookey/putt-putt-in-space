using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ellipse2 {

    [Range(1, 25)]
    public float radiusX = 1, radiusY = 2;

    [Range(10, 90)]
    public int smoothness = 30;

    [Range(0, 180)]
    public int rotation = 0;

    public Transform trans;
    public Vector2 offset;

    Vector2 origin, center;

    public Vector2 GetPoint(float factor) {
        center = origin + offset;

        float ang = 360 * factor;
        float o = rotation * Mathf.Deg2Rad;

        float a = ang * Mathf.Deg2Rad;
        float x = center.x + radiusX * Mathf.Cos(a) * Mathf.Cos(o) - radiusY * Mathf.Sin(a) * Mathf.Sin(o);
        float y = center.y - radiusX * Mathf.Cos(a) * Mathf.Sin(o) - radiusY * Mathf.Sin(a) * Mathf.Cos(o);

        return new Vector2(x, y);
    }

    // The angle returned is the signed acute clockwise angle between the two vectors.
    public float GetFactor(Vector2 contactPoint) {
        center = origin + offset;
        Vector2 from = Vector2.up;
        Vector2 to = contactPoint - center;

        float ang = Vector2.SignedAngle(from, to);
        ang = (360 + ang - rotation) % 360;

        return ang / 360f;
    }

    public Vector2 Origin { get; set; }
    public Vector2 Offset { get; set; }


}
