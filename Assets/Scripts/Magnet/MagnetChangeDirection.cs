using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetChangeDirection : MonoBehaviour {

    // Public Variables
    public Vector2 magnetDirection;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Magnet") {
            collision.GetComponent<MagnetAction>().newPosition = magnetDirection;
        }
    }
}
