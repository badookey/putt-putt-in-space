using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour {

    public float power = 1;
    public float deadzoneRadius = 500;  // == maximum effect area
    public bool forward = false;

    Rigidbody2D rb;

    bool tap;
    bool isDragging = false;
    Vector2 startTouch, dragDelta;

    void Start() {
        rb = GetComponent<Rigidbody2D>();    
    }

    void Update() {

        if (Input.GetMouseButtonDown(0)) {
            startTouch = Input.mousePosition;
        } else if (Input.GetMouseButtonUp(0)) {
            dragDelta = (Vector2)Input.mousePosition - startTouch;
            
            if (dragDelta.magnitude < deadzoneRadius) {
                if (forward) {
                    rb.AddForce(dragDelta * power);
                } else {
                    rb.AddForce(-1f * dragDelta * power);
                }
                Debug.Log(string.Format("Force volum: {0}", dragDelta.magnitude));
            } else {
                Debug.Log(string.Format("Force lease: {0}", dragDelta.magnitude));
            }

            Reset();
        }
    }


    void Reset() {
        startTouch = dragDelta = Vector2.zero;
        isDragging = false;
    }
}
