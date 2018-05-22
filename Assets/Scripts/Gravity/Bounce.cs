using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bounce : MonoBehaviour {
    public Rigidbody2D rigidBody;
    // Use this for initialization
    void Start () {
		
	}
    void OnTriggerEnter2D(Collider2D col)
    {
        Vector3 pos = col.gameObject.transform.position;
        if(Math.Abs(pos.y)>10)
        {
            Vector2 vector = rigidBody.velocity;
            rigidBody.velocity = new Vector2(vector.x, -vector.y);
        }
        if (Math.Abs(pos.x) > 17)
        {
            Vector2 vector = rigidBody.velocity;
            rigidBody.velocity = new Vector2(-vector.x, vector.y);
        }
    }
        // Update is called once per frame
        void Update () {
		
	}
}
