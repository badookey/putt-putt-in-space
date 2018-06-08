using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEditor;

public class Exit : MonoBehaviour {
    public string nextscenename;
	// Use this for initialization
	void Start () {
        Time.timeScale = 1;
	}
    private Rect windowRect = new Rect((Screen.width - 200) / 2, (Screen.height - 300) / 2, 200, 300);
    // Only show it if needed.
    private bool show = false;
    private bool menuShow = false;

    void OnGUI()
    {
        if (show)
            windowRect = GUI.Window(0, windowRect, DialogWindow, "You completed "+ SceneManager.GetActiveScene().name);
        if(menuShow)
            windowRect = GUI.Window(0, windowRect, MenuWindow, "Paused - " + SceneManager.GetActiveScene().name);
    }
    void MenuWindow(int windowID)
    {
        Time.timeScale = 0;
        float y = 20;
        GameObject gameObject = GameObject.Find("Player");
        AccumulationMovement movement = gameObject.GetComponent<AccumulationMovement>();

        if (GUI.Button(new Rect(5, y, windowRect.width - 10, 20), "Restart Level"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1;
            menuShow = false;
        }
        if (GUI.Button(new Rect(5, 2 * y+2, windowRect.width - 10, 20), "***DEBUG LEVEL SELECT***"))
        {
            SceneManager.LoadScene("LevelSelect");
            Time.timeScale = 1;
            menuShow = false;
        }
        if (GUI.Button(new Rect(5, 3 * y + 2, windowRect.width - 10, 20), "Input mode - "+movement.drageMode))
        {
            movement.SwapDragMode();


        }
        if (GUI.Button(new Rect(5, 5 * y+2, windowRect.width - 10, 20), "Main Menu"))
        {
            SceneManager.LoadScene("Menu");
            Time.timeScale = 1;
            menuShow = false;
        }
        if (GUI.Button(new Rect(5, 6 * y+2, windowRect.width - 10, 20), "Exit Game"))
        {
            Application.Quit();
            Time.timeScale = 1;
            menuShow = false;
        }
        

    }
    // This is the actual window.
    void DialogWindow(int windowID)
    {
        Time.timeScale = 0;
        float y = 20;
        float[] a = HighScore.Save(SceneManager.GetActiveScene().name);
        GUI.Label(new Rect(5, y, windowRect.width, 20), string.Format("Your score: {0} Time: {1}", (int)a[0], a[1]));
        GUI.Label(new Rect(5, 2*y, windowRect.width, 20), string.Format("High score: {0} Time: {1}", (int)a[2], a[3]));

        if (GUI.Button(new Rect(5, 4*y+2, windowRect.width - 10, 20), "Replay Level"))
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            
            menuShow = false;
        }
        if (GUI.Button(new Rect(5, 3*y+2, windowRect.width - 10, 20), "Next Level"))
        {
            SceneManager.LoadScene(nextscenename);
            Time.timeScale = 1;
            show = false;
        }
        if (GUI.Button(new Rect(5, 5*y+2, windowRect.width - 10, 20), "Main Menu"))
        {
            SceneManager.LoadScene("Menu");
            Time.timeScale = 1;
            show = false;
        }
        if (GUI.Button(new Rect(5, 6*y+2, windowRect.width - 10, 20), "Exit"))
        {
            Application.Quit();
            Time.timeScale = 1;
            show = false;
        }
    }
    public void GameOver()
    {
        show = true;
        //GameObject gameObject = GameObject.Find("Victory");
        //gameObject.SetActive(true);
        /*
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
                */
    }
        
    
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(menuShow)
                Time.timeScale = 1;
            menuShow = !menuShow;
            
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        GameOver();
    }
}
