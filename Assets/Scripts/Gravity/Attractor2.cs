using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Attractor2 : MonoBehaviour {

    const float G = 667.408f;  // gravitational constant = 6.67408 × 10-11 m3 kg-1 s-2

    [SerializeField]
    List<AttractorPlanet> aPlanets;

    [SerializeField]
    bool isLocked = false;
    Transform lockPlanet;
    Transform lastLockPlanet;
    Rigidbody2D rb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();    
    }

    void OnEnable() {
        if (aPlanets == null) { aPlanets = new List<AttractorPlanet>(); }    

        // load all planets when start
        foreach (GameObject planets in GameObject.FindGameObjectsWithTag("Planet")) {
            aPlanets.Add(planets.GetComponent<AttractorPlanet>());
        }
    }

    private void OnDisable() {
        aPlanets.Clear();
    }

    void FixedUpdate() {
        if (isLocked) {
            // orbit
            Vector2 direction = rb.position - (Vector2)lockPlanet.gameObject.transform.position;
            float distance = direction.magnitude;
            if (distance != 0) {
                float forceMagnitue = G * (rb.mass * lockPlanet.gameObject.GetComponent<AttractorPlanet>().mass) / Mathf.Pow(distance, 2);
                Vector2 force = direction.normalized * forceMagnitue;
                rb.AddForce(-1f * force);
            }
        } else {
            // compulte next planet to orbit
            ComputeGravitation();
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            // release activelly
            Reset();
        }

    }

    void ComputeGravitation() {

        foreach (AttractorPlanet ap in aPlanets) {

            if (ap.gameObject == lastLockPlanet) {
                continue;
            }

            Vector2 direction = rb.position - (Vector2) ap.gameObject.transform.position;
            float distance = direction.magnitude;

            if (distance == 0) {
                continue;
            }

            float forceMagnitue = G * (rb.mass * ap.mass) / Mathf.Pow(distance, 2);
            Vector2 force = direction.normalized * forceMagnitue;
            rb.AddForce(-1f * force);
        }

    }
    
    void OnTriggerEnter2D(Collider2D other) {
        if (isLocked)
            return;

        isLocked = true;
        lockPlanet = other.transform;
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject == lockPlanet.gameObject) {
            Reset();
        }
    }

    void Reset() {
        isLocked = false;
        lastLockPlanet = lockPlanet;
        lockPlanet = null;
    }
}
