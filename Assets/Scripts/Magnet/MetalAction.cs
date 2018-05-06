using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalAction : MonoBehaviour {

    // Script for managing metal movement

    // Public variables
    public float magnetStrength = 5.0f;
    public float distanceStretch = 10.0f;  // strength, based on distance
    public int magnetDirection = 1;  // 1 = attact, -1 = repel
    public bool looseMagnet = true;

    // Private variables
    private Transform trans;
    private Rigidbody2D rd;
    private Transform magnetTrans;
    private bool magnetInZone;

    private void Awake() {
        trans = transform;
        rd = trans.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {

        if (magnetInZone) {
            Vector2 directionToMagnet = magnetTrans.position - trans.position;  // control attact/repel
            float distance = Vector2.Distance(magnetTrans.position, trans.position);  // control strength
            float magnetDistanceStr = (distanceStretch / distance) * magnetStrength;

            rd.AddForce(magnetDistanceStr * (directionToMagnet * magnetDirection));

        }


    }


    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Magnet") {
            magnetTrans = collision.transform;
            magnetInZone = true;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Magnet" && looseMagnet) {
            magnetInZone = false;
        }
    }

}
