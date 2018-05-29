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
        else {
            foreach (OrbitRingObjectMotion orom in orbitRing) {
                

            }
        }
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

            // do something
        }
    }

    public Vector2 Center {
        get { return transform.position; }
    }
}
