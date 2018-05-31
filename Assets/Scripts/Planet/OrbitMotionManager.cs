using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitMotionManager : MonoBehaviour {

    [Range(0, 360)]
    public int orbitSpeed = 36;
    public float orbitRadius = 5;
    public List<OrbitRingObjectMotion> orbitRing;

    private CircleCollider2D cc;
    
    private void Start() {
        cc = GetComponent<CircleCollider2D>();
        cc.radius = orbitRadius;

        if (orbitRing == null)
            orbitRing = new List<OrbitRingObjectMotion>();
    }


    private void Update() {

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            OrbitMotion otherOm = other.GetComponent<OrbitMotion>();
            
            // do something
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player") {
            OrbitMotion otherOm = other.GetComponent<OrbitMotion>();
            Rigidbody2D otherRb = other.GetComponent<Rigidbody2D>();

            // do something
            float speed = Mathf.Deg2Rad * orbitRadius / Time.fixedDeltaTime;
            switch (otherOm.orbitSpeedMode) {
                case OrbitSpeedMode.auto:
                    speed *= orbitSpeed;
                    break;
                case OrbitSpeedMode.relative:
                    speed *= ((otherOm.orbitSpeed + orbitSpeed) % 360);
                    break;
                default:
                    speed *= otherOm.orbitSpeed;
                    break;
            }

            otherRb.AddForce(EvaluateExitDirection(otherOm) * speed);
        }
    }

    private Vector2 EvaluateExitDirection(OrbitMotion om) {
        
        Vector2 exitDir = Vector2.zero;
        Vector2 vecToTarget = transform.position - om.transform.position;

        switch (om.orbitDirection) {
            case OrbitDirection.clockwise:
                // rotate -90
                exitDir = Quaternion.Euler(0, 0, 90f) * vecToTarget;
                break;
            case OrbitDirection.counterClockwise:
                // rotate 90
                exitDir = Quaternion.Euler(0, 0, -90f) * vecToTarget;
                break;
        }
        return exitDir.normalized;
    }
    
    public Vector2 Center {
        get { return transform.position; }
    }
}
