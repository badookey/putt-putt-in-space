using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularGravity : MonoBehaviour {

    [SerializeField]
    Transform planet;
    Rigidbody2D rb;

    [SerializeField]
    float gravitationPull;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        rb.AddForce(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));

        // release
        if (Input.GetKeyDown(KeyCode.Space)) {
            // release
            Debug.Log("i was released");
            planet = null;
        }

        if (planet != null) {
            // move
            Vector2 difference = planet.transform.position - this.transform.position;
            rb.AddForce(difference.normalized * gravitationPull);

            // rotate
            float angle = Mathf.Atan2(difference.y, difference.x);
            this.transform.rotation = Quaternion.AngleAxis(angle, transform.up);
        } else {
            // keep going
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Planet") {
            planet = other.gameObject.transform;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Planet") {
            planet = null;
        }
    }
}
