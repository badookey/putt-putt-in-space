﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Continue : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
    void OnMouseDown()
    {
        SceneManager.LoadScene("Demo v1.2");
    }
    // Update is called once per frame
    void Update () {
		
	}
}
