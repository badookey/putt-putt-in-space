using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccumulationMovement : MonoBehaviour {

    public float power;
    public float threshold;
    public DragMode drageMode;

    private Vector2 startPos;

    private Rigidbody2D rb;
    private OrbitMotion om;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        om = GetComponent<OrbitMotion>();
    }



    private void Update() {

        if (Input.GetMouseButtonDown(0)) {  // start
            startPos = Input.mousePosition;
        } else if (Input.GetMouseButton(0)) {  // holding
            //Debug.Log("accumulation: " + ((Vector2)Input.mousePosition - startPos).magnitude);
        } else if (Input.GetMouseButtonUp(0)) {  // finish
            Vector2 endPos = transform.position;
            Vector2 newForce = (Vector2)Input.mousePosition - startPos;

            
            if (newForce.magnitude < threshold) {  // acceptable force

                if (om.Active) {
                    // *********************
                    // release from orbiting
                    // *********************
                    Debug.Log("accumulation: " + ((Vector2)Input.mousePosition - startPos).magnitude);
                    switch (drageMode) {
                        case DragMode.forward:
                            rb.AddForce(newForce * power);
                            break;
                        case DragMode.backward:
                            rb.AddForce(-1.0f * newForce * power);
                            break;
                    }

                } else {
                    switch (drageMode) {
                        case DragMode.forward:
                            rb.AddForce(newForce * power);
                            break;
                        case DragMode.backward:
                            rb.AddForce(-1.0f * newForce * power);
                            break;
                    }
                }
            }
        }
        
    }

}

public enum DragMode {
    forward, backward
}