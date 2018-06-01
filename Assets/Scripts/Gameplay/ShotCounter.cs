using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShotCounter : MonoBehaviour {
    public Text text;
    AccumulationMovement movement;
    // Use this for initialization
    void Start () {
        GameObject gameObject = GameObject.Find("Player");
    
     movement = gameObject.GetComponent<AccumulationMovement>();
}
	
	// Update is called once per frame
	void Update () {
        int i = movement.ValidHits;
        text.text=string.Format("Shot Count: {0}", i);
	}
}
