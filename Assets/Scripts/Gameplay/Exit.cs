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
    private Rect windowRect = new Rect(0, 0, (int)((Screen.width)), (int)((Screen.height)));
    // Only show it if needed.
    private bool show = false;
    private bool menuShow = false;
    private GameObject audio;
    

    void OnGUI()
    {
        GUIStyle windowStyle = new GUIStyle(GUI.skin.window);
        windowStyle.fontSize = (int)(30);
        if (show)
            windowRect = GUI.Window(0, windowRect, DialogWindow, "You completed "+ SceneManager.GetActiveScene().name,windowStyle);
        if(menuShow)
            windowRect = GUI.Window(1, windowRect, MenuWindow, "Paused - " + SceneManager.GetActiveScene().name,windowStyle);
    }
    //PlayerPrefs.SetInt("dragmode", (int) drageMode);
    //PlayerPrefs.Save();
    void MenuWindow(int windowID)
    {
        Time.timeScale = 0;
        float y = (float)(windowRect.height / 10);
        GUIStyle myStyle = new GUIStyle(GUI.skin.button);
        myStyle.fontSize = (int)(40);
        GameObject gameObject = GameObject.Find("Player");
        AccumulationMovement movement = gameObject.GetComponent<AccumulationMovement>();
        audio = GameObject.FindGameObjectWithTag("Audio");
        string x;
        if (!audio.GetComponent<AudioSource>().mute)
        {
            x = "enabled";
        }
        else
        {
            x = "disabled";
        }

        if (GUI.Button(new Rect(5, y, windowRect.width - 10, windowRect.height / 10), "Restart Level",myStyle))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1;
            menuShow = false;
        }
        if (GUI.Button(new Rect(5, 2 * y+15, windowRect.width - 10, windowRect.height / 10), "Level Select",myStyle))
        {
            SceneManager.LoadScene("LevelSelect");
            Time.timeScale = 1;
            menuShow = false;
        }
        if (GUI.Button(new Rect(5, 3 * y + 30, windowRect.width - 10, windowRect.height / 10), "Input mode - "+movement.drageMode,myStyle))
        {
            movement.SwapDragMode();


        }
        GUI.Label(new Rect(5, 5 * y + 60, windowRect.width - 10, windowRect.height / 10), "Input Sensitivity");
        GUI.skin.label.alignment = TextAnchor.MiddleCenter;
        float sensitivty = GUI.HorizontalSlider(new Rect(5, 6 * y + 75, windowRect.width - 10, windowRect.height / 10), PlayerPrefs.GetFloat("sensitivity"), (float)0.1, (float)1.5);

        PlayerPrefs.SetFloat("sensitivity", sensitivty);
        PlayerPrefs.Save();


        if (GUI.Button(new Rect(5, 7 * y+90, windowRect.width - 10, windowRect.height / 10), "Main Menu",myStyle))
        {
            SceneManager.LoadScene("Menu");
            Time.timeScale = 1;
            menuShow = false;
        }
        if (GUI.Button(new Rect(5, 4 * y+45, windowRect.width - 10, windowRect.height/10), "Music "+x,myStyle))
        {
            audio = GameObject.FindGameObjectWithTag("Audio");
            audio.GetComponent<AudioSource>().mute = !audio.GetComponent<AudioSource>().mute;
        }
        if (GUI.Button(new Rect(5, 8 * y + 105, windowRect.width - 10, windowRect.height / 10), "Exit Game", myStyle))
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
        float y = windowRect.height / 10;
        GUIStyle myStyle = new GUIStyle(GUI.skin.button);
        GUIStyle labelStyle = new GUIStyle(GUI.skin.label);
        myStyle.fontSize = (int)(40);
        labelStyle.fontSize = (int)40;
        float[] a = HighScore.Save(SceneManager.GetActiveScene().name);
        GUI.Label(new Rect(5, y , windowRect.width - 10, windowRect.height / 10), string.Format("Your score: {0} Time: {1}", (int)a[0], a[1]),labelStyle);
        GUI.Label(new Rect(5, 2*y, windowRect.width - 10, windowRect.height / 10), string.Format("High score: {0} Time: {1}", (int)a[2], a[3]),labelStyle);

        if (GUI.Button(new Rect(5, 4 * y+30, windowRect.width - 10, windowRect.height / 10), "Replay Level",myStyle))
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            
            menuShow = false;
        }
        if (GUI.Button(new Rect(5, 3 * y+15, windowRect.width - 10, windowRect.height / 10), "Next Level",myStyle))
        {
            SceneManager.LoadScene(nextscenename);
            Time.timeScale = 1;
            show = false;
        }
        if (GUI.Button(new Rect(5, 5 * y+45, windowRect.width - 10, windowRect.height / 10), "Main Menu",myStyle))
        {
            SceneManager.LoadScene("Menu");
            Time.timeScale = 1;
            show = false;
        }
        if (GUI.Button(new Rect(5, 6 * y+60, windowRect.width - 10, windowRect.height / 10), "Exit",myStyle))
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
