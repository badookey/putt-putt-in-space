using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class Exit : MonoBehaviour {
    public string nextscenename;
	// Use this for initialization
	void Start () {
		
	}
    public void GameOver()
    {
        //GameObject gameObject = GameObject.Find("Victory");
        //gameObject.SetActive(true);
        float[] a = HighScore.Save(SceneManager.GetActiveScene().name);
        int option = EditorUtility.DisplayDialogComplex("You won!",string.Format("Your score: {0} Time: {1} \n High score: {2} Time: {3}",(int)a[0],a[1],(int)a[2],a[3]),
                "Next Level",
                "Main Menu",
                "Exit Game");

        switch (option)
        {
            // Save Scene
            case 0:
                SceneManager.LoadScene(nextscenename);
                break;

            // Save and Quit.
            case 1:
                SceneManager.LoadScene("Menu");
                break;

            // Quit Without saving.
            case 2:
                EditorApplication.Exit(0);
                break;

            default:
                Debug.LogError("Unrecognized option.");
                break;
        }
        
    }
    // Update is called once per frame
    void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        GameOver();
    }
}
