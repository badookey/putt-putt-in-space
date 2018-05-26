using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivityLimitationForTest : MonoBehaviour {

    public int minX;
    public int maxX;
    public int minY;
    public int maxY;

    Rigidbody2D rb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update () {

        float x = transform.position.x;
        float y = transform.position.y;

        if (x > maxX || x < minX || y < minY || y > maxY)
            rb.velocity = Vector3.zero;

    }
    
}
