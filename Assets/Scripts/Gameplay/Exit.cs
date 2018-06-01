using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
    public void GameOver()
    {
        GameObject gameObject = GameObject.Find("Player");
        AccumulationMovement movement = gameObject.GetComponent<AccumulationMovement>();
        movement.ValidHits = 0;
        HighScore.Save(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("Victory");
    }
    // Update is called once per frame
    void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        GameOver();
    }
}
