using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceAffectedObject : ForceObject {

    public Rigidbody2D rb;
    public float mass = 1;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        rb.mass = mass;
        rb.gravityScale = 0;
    }
}
