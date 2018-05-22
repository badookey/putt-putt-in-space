using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractorPlanet : MonoBehaviour {

    public float mass;

    Rigidbody2D rb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        if (rb != null) {
            mass = rb.mass;
        }
    }
}
