using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    // Public variables
    public GameObject metal;

    //Private varibales
    private Rigidbody2D metalRb;

    private void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Vector2 mousePos = Input.mousePosition;
            Vector2 instancePos = Camera.main.ScreenToWorldPoint(mousePos);

            Instantiate(metal, instancePos, Quaternion.identity);
        }
    }

}
