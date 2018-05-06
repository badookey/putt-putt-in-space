using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetAction : MonoBehaviour {

    // Script for managing magnet movement

    // Public Variables
    public Vector2 newPosition;  // The target position
    public float moveSpeed;

    // Private Variables
    private Transform trans;
    private float limitDistance = 0.05f;

    private void Awake() {
        trans = transform;
    }

    private void Update() {
        trans.position = Vector3.Lerp(trans.position, newPosition, Time.deltaTime * moveSpeed);

        // Fix pos when extremly close
        if (Mathf.Abs(newPosition.x - trans.position.x) < limitDistance) {
            trans.position = newPosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Metal") {
            Destroy(collision.gameObject);
        }
    }
}
