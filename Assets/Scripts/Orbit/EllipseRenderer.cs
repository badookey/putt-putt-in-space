using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class EllipseRenderer : MonoBehaviour {

    LineRenderer lr;

    [Range(3, 36)]
    public int segments;
    public Ellipse ellipse;

    void Awake() {
        lr = GetComponent<LineRenderer>();
        CalculateEllipse();
    }

    void CalculateEllipse() {
        Vector3[] points = new Vector3[segments + 1];  // 最后与最前相同

        for (int i = 0; i < segments; i++) {
            Vector2 position2D = ellipse.evaluate((float)i / (float)segments);
            points[i] = new Vector3(position2D.x, position2D.y, 0f);
        }

        points[segments] = points[0];

        lr.positionCount = segments + 1;
        lr.SetPositions(points);
    }

    void OnValidate() {  // run befor awake
        if (Application.isPlaying && lr != null)
            CalculateEllipse();    
    }
}
