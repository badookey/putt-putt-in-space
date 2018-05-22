using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class EllipseRenderer : MonoBehaviour {

    [Range(3, 90)]
    public int segments = 30;
    public Ellipse ellipse;
    public bool refresh = true;  // as a refresh button

    LineRenderer lr;
    Vector2 offset;

    void Awake() {
        lr = GetComponent<LineRenderer>();
        lr.widthMultiplier = 0.1f;

        CalculateEllipse();
    }

    void CalculateEllipse() {
        Vector3[] points = new Vector3[segments + 1];  // +1 for making a ring

        for (int i = 0; i < segments; i++) {
            Vector2 position2D = ellipse.Evaluate((float)i / (float)segments);
            points[i] = new Vector3(position2D.x + offset.x, position2D.y + offset.y, 0f);
        }

        points[segments] = points[0];

        lr.positionCount = segments + 1;
        lr.SetPositions(points);
    }

    void OnValidate() {  // run before awake
        if (Application.isPlaying && lr != null) {
            offset = transform.position;
            CalculateEllipse();
            refresh = true;
        }
    }
}
