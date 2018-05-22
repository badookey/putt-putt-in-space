using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ellipse {

    public float xAxis;
    public float yAxis;

    public Ellipse(float xAxis, float yAxis) {
        this.xAxis = xAxis;
        this.yAxis = yAxis;
    }

    public Vector2 Evaluate(float t) {
        float angle = Mathf.Deg2Rad * 360f * t;
        float x = Mathf.Sin(angle) * xAxis;
        float y = Mathf.Cos(angle) * yAxis;
        return new Vector2(x, y);
    }


    // calculate the progress of the point on ellipse(clockwise) 
    public float AntiEvaluate(float x, float y) {
        float xAngle = Mathf.Asin((x / xAxis)) * Mathf.Rad2Deg;
        float yAngle = Mathf.Acos((y / yAxis)) * Mathf.Rad2Deg;

        if (xAngle > 0) {
            if (yAngle < 90) {  // 1st quadrant
                return yAngle / 360f;
            } else {  // 2nd quadrant
                return yAngle / 360f;
            }
        } else {
            if (yAngle < 90) {  // 3rd quadrant
                return 1f - yAngle / 360f;
            } else {  // 4th quadrant
                return 1f - yAngle / 360f;
            }
        }
    }
}
