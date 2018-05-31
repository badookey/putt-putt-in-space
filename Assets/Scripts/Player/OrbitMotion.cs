using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitMotion : MonoBehaviour {

    public OrbitSpeedMode orbitSpeedMode;
    [Range(0, 360)]
    public int orbitSpeed = 36;
    public OrbitDirection orbitDirection;  // default

    private Rigidbody2D rb;
    private ForceObject fo;
    private OrbitMotionManager omm;

    private bool _active = false;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        fo = GetComponent<ForceObject>();
    }

    private void Update() {
        if (_active && omm != null) {
            int speed = 0;
            switch (orbitSpeedMode) {
                case OrbitSpeedMode.auto:
                    speed = omm.orbitSpeed;
                    break;
                case OrbitSpeedMode.relative:
                    speed = ((omm.orbitSpeed + orbitSpeed) % 360);
                    break;
                default:
                    speed = orbitSpeed;
                    break;
            }

            switch (orbitDirection) {
                case OrbitDirection.clockwise:
                    transform.RotateAround(omm.Center, Vector3.back, speed * Time.deltaTime);
                    break;
                case OrbitDirection.counterClockwise:
                    transform.RotateAround(omm.Center, Vector3.forward, speed * Time.deltaTime);
                    break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {

        if (other.tag == "Planet_Orbit_Ring") {
            // pause force system
            if (fo != null)
                fo.active = false;

            // evaluate orbiting direction
            orbitDirection = EvaluateOrbitDirection(other.transform);

            // stop moving
            rb.velocity = Vector2.zero;

            // start orbiting
            omm = other.GetComponent<OrbitMotionManager>();
            _active = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Planet_Orbit_Ring") {

            // resume force system
            if (fo != null)
                fo.active = true;
            
            // stop orbiting
            ResetOrbit();
        }
    }

    private void ResetOrbit() {
        omm = null;
        _active = false;
    }

    private OrbitDirection EvaluateOrbitDirection(Transform target) {
        Vector2 vecToTarget = target.position - transform.position;
        if (Vector2.SignedAngle(vecToTarget, rb.velocity) >= 0)
            return OrbitDirection.clockwise;
        else 
            return OrbitDirection.counterClockwise;
    }

    public bool Active {
        get { return _active; }
        set { _active = value; }
    }
    
}

public enum OrbitSpeedMode {
    auto, absolute, relative
}

public enum OrbitDirection {
    clockwise, counterClockwise
}