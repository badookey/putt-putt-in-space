using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour {

    

    // Public variables
    public Rigidbody2D rb;
    public static List<Attractor> Attractors;
    public static List<Attractor> AllAttractors;
    public bool shouldAttract = false;
    public float G = 667.408f;  // gravitational constant = 6.67408 × 10-11 m3 kg-1 s-2


    private void OnEnable() {
        if (Attractors == null) {
            Attractors = new List<Attractor>();
        }
        if (AllAttractors == null)
        {
            AllAttractors = new List<Attractor>();
        }
        //Attractors.Add(this);
        AllAttractors.Add(this);
    }

    private void OnDisable() {
        Attractors.Remove(this);
    }
    private void OnMouseDown()
    {
            if (Attractors.Contains(this))
            {
                Attractors.Remove(this);
                this.shouldAttract = false;
            }
            else
            {
                Attractors.Add(this);
                this.shouldAttract = true;
            }

    }

    public void Update()
    {
        
        
    }
    private void FixedUpdate() {
        foreach (Attractor attractor in Attractors) {
            if (attractor != this && attractor.shouldAttract == true) {
                Attract(attractor);
            }
        }

    }

    void Attract(Attractor objToAttract) {
        Rigidbody2D rbToAttact = objToAttract.rb;

        Vector2 direction = rb.position - rbToAttact.position;
        float distance = direction.magnitude;

        if (distance == 0) { 
            return;
        }

        float forceMagnitue = G * (rb.mass * rbToAttact.mass) / Mathf.Pow(distance, 2);
        Vector2 force = direction.normalized * forceMagnitue;

        rbToAttact.AddForce(force);
    }
}
