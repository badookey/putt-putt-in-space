using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinScript : MonoBehaviour {

    public bool active;
    public SpinDirection spinDirection;
    public float speed;
    [ReadOnly]
    public float angle;
	
	void Update () {
        if (active) {
            switch (spinDirection) {
                case SpinDirection.clockwise:
                    transform.Rotate(Vector3.back * Time.deltaTime * speed);
                    break;
                case SpinDirection.counter_clockwise:
                    transform.Rotate(Vector3.forward * Time.deltaTime * speed);
                    break;
            }
        }
    }

    private void LateUpdate() {
        angle = CalculateRotateAngle(transform);
    }

    // cacluate angle in clockwise
    public static float CalculateRotateAngle(Transform trans) {
        return 360f - trans.rotation.eulerAngles.z;
    }
}

public enum SpinDirection {
    clockwise, counter_clockwise
}