using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitRingObjectMotion : MonoBehaviour {

	private bool active;
    public OrbitSpeedMode orbitSpeedMode;
    [Range(0, 360)]
    public int orbitSpeed;
    public OrbitDirection orbitDirection;
    private OrbitMotionManager omm;

    private void Start() {
        Transform orbitRingObjects = transform.parent;
        Transform planet = orbitRingObjects.parent;
        omm = planet.GetComponentInChildren<OrbitMotionManager>();
        omm.orbitRing.Add(this);
    }

    private void Update() {
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
