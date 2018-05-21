using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowScript : MonoBehaviour {

    [Range(0, 1)]
    public float power = 0;

    Rigidbody2D rb;
    
    Vector2 startPoint;
    Vector2 endPoint;

    [SerializeField]
    bool isDragging = false;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnMouseDown() {
        startPoint = Camera.main.WorldToScreenPoint(Input.mousePosition);
    }

    void OnMouseUp() {

        if (!isDragging) {
            return;
        }

        endPoint = Camera.main.WorldToScreenPoint(Input.mousePosition);
        Vector2 difference = startPoint - endPoint;
        Debug.Log(difference.normalized);
        rb.AddForce(difference * power);

        isDragging = false;

    }

    void OnMouseDrag() {
        
        isDragging = true;
    }

}
