using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class OrbitRingRenderer : MonoBehaviour {

    [Range(10, 90)]
    public int smoothness = 30;

    private LineRenderer lr;
    private float radius;
    private OrbitMotionManager omm;
    private CircleCollider2D player;
    private Vector3[] points;
   
    private void Start() {
        omm = GetComponent<OrbitMotionManager>();
        radius = omm.orbitRadius;

        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) {
            player = p.GetComponent<CircleCollider2D>();
            points = GetPoints(player.radius);
        } else {
            points = GetPoints(0);
        }
        ShowLine(0.1f);
    }

    private void ShowLine(float width) {
        if (lr == null)
            lr = GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Sprites/Default"));

        // positions
        lr.positionCount = points.Length;
        lr.SetPositions(points);

        // size
        lr.widthMultiplier = width;

        // color
        float alpha = 1.0f;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Color.green, 0.0f), new GradientColorKey(Color.red, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
            );
        lr.colorGradient = gradient;
        
        
    }

    private Vector3[] GetPoints(float off) {

        List<Vector3> points = new List<Vector3>();

        float r = radius + off;
        Vector2 center = transform.position;

        float ang = 0;

        for (int i = 0; i <= smoothness; i++) {
            float a = ang * Mathf.Deg2Rad;
            float x = center.x + r * Mathf.Cos(a);
            float y = center.y - r * Mathf.Sin(a);

            points.Add(new Vector3(x, y, 0f));
            ang += 360f / smoothness;
            Debug.Log(ang);
        }

        points.Add(points[0]);  // make a ring

        return points.ToArray();
    }


    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            ShowLine(0.3f);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player") {
            ShowLine(0.1f);
        }
    }
}
