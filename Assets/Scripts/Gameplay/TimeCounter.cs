using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCounter : MonoBehaviour {
    public Text text;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float f = Time.timeSinceLevelLoad;
        text.text = ("Time Elapsed: " + f.ToString("n2"));
    }
}
