using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitRingObjectMotion : MonoBehaviour {

	public bool active = true;
    public Vector2 startPosition;
    public OrbitSpeedMode orbitSpeedMode;
    [Range(0, 360)]
    public int orbitSpeed = 36;
    public OrbitDirection orbitDirection;

    private OrbitMotionManager omm;

    private void Start() {

        if (transform.position == Vector3.zero) {
            transform.position = startPosition;
        }

        Transform orbitRingObjects = transform.parent;
        Transform planet = orbitRingObjects.parent;
        omm = planet.GetComponentInChildren<OrbitMotionManager>();
        omm.orbitRing.Add(this);
    }

    private void Update() {
        if (!active)
            return;

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
