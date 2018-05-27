using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccumulationMovement : MonoBehaviour {

    public float power = 10f;
    public float dragThreshold = 500f;  // max activity area
    public bool forward = false;  // move along with drag direction

    public bool isDragging = false;
    public bool isValidDragging = false;

    public Vector2 dragDelta;
    public float accumulation = 0f;

    Rigidbody2D rb;
    Vector2 startTouch;

    void Start() {
        rb = GetComponent<Rigidbody2D>();    
    }

    void Update() {

        if (Input.GetMouseButtonDown(0)) {  // begin accumulation
            isDragging = true;
            startTouch = Input.mousePosition;
        } else if (Input.GetMouseButtonUp(0)) {
            if (isValidDragging) {
                // finish accumulation & shoot
                if (forward) {
                    rb.AddForce(dragDelta * power);
                } else {
                    rb.AddForce(-1f * dragDelta * power);
                }
            } else {
                // cancel accumulation
            }
            Reset();
        }

        if (isDragging) {
            dragDelta = (Vector2)Input.mousePosition - startTouch;
            accumulation = dragDelta.magnitude;
            isValidDragging = accumulation < dragThreshold ? true : false;
            
            // ******************************
            // DO SOMETHING INDICATING PLAYER
        }
    }    
    void Reset() {
        startTouch = dragDelta = Vector2.zero;
        isDragging = isValidDragging = false;
        accumulation = 0f;
    }
}
