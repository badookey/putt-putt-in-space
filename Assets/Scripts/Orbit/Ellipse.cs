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

    public Vector2 evaluate(float t) {
        float angle = Mathf.Deg2Rad * 360f * t;
        float x = Mathf.Sin(angle) * xAxis;
        float y = Mathf.Cos(angle) * yAxis;
        return new Vector2(x, y);
    }


    // 返回顺时针位置百分比
    public float antiEvaluate(float x, float y) {
        float xAngle = Mathf.Asin((x / xAxis)) * Mathf.Rad2Deg;
        float yAngle = Mathf.Acos((y / yAxis)) * Mathf.Rad2Deg;

        if (xAngle > 0) {
            if (yAngle < 90) {  // 第一象限
                return yAngle / 360f;
            } else {  // 第二象限
                return yAngle / 360f;
            }
        } else {
            if (yAngle < 90) {  // 第四象限
                return 1f - yAngle / 360f;
            } else {  // 第三象限
                return 1f - yAngle / 360f;
            }
        }
    }
}
