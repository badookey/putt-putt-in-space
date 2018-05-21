using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{

    public Rigidbody2D rigidBody;
    public int speed;
    public int m_Speed = 2;
    // Use this for initialization
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = new Vector3(0, speed, 0);
    }

    // Update is called once per frame
    void Update()
    {
        var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");
        if (Input.GetKey(KeyCode.UpArrow))
        {
            //Move RigidBody upwards
            rigidBody.AddForce( Vector2.up * m_Speed);
        }

        //Press the Down arrow key to move the RigidBody downwards
        if (Input.GetKey(KeyCode.DownArrow))
        {
            //Move RigidBody downwards
            rigidBody.AddForce(Vector2.down * m_Speed);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            //Move RigidBody upwards
            rigidBody.AddForce(Vector2.left * m_Speed);
        }

        //Press the Down arrow key to move the RigidBody downwards
        if (Input.GetKey(KeyCode.RightArrow))
        {
            //Move RigidBody downwards
            rigidBody.AddForce(Vector2.right * m_Speed);
        }

    }
}
