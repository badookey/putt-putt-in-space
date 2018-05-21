using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour {


    public float deadzoneRadius = 125f;

    bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;
    bool isDragging = false;
    Vector2 startTouch, swipeDelta;

    void Update() {
        tap = swipeLeft = swipeRight = swipeUp = swipeDown = false;

        if (Input.GetMouseButtonDown(0)) {
            tap = true;
            isDragging = true;
            startTouch = Input.mousePosition;
        } else if (Input.GetMouseButtonUp(0)) {
            isDragging = false;
            Reset();
        }

        // Calculate the distance
        swipeDelta = Vector2.zero;
        if (isDragging) {
            if (Input.GetMouseButton(0)) {
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
            }
        }

        // Cross deadzone
        if (swipeDelta.magnitude > deadzoneRadius) {
            // Direction
            float x = swipeDelta.x;
            float y = swipeDelta.y;

            if (Mathf.Abs(x) > Mathf.Abs(y)) {
                // Left/right
                if (x < 0) {
                    swipeLeft = true;
                } else {
                    swipeRight = true;
                }
            } else {
                // Up/down
                if (y < 0) {
                    swipeDown = true;
                } else {
                    swipeUp = true;
                }
            }

            Reset();
        }
    }

    void Reset() {
        startTouch = swipeDelta = Vector2.zero;
        isDragging = false;
    }

    public bool Tap { get { return tap; } }
    public bool SwipeLeft { get { return swipeLeft; } }
    public bool SwipeRight { get { return swipeRight; } }
    public bool SwipeUp { get { return swipeUp; } }
    public bool SwipeDown { get { return swipeDown; } }
    public Vector2 SwipeDelta { get { return swipeDelta; } }
}
