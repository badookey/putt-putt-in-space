using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public void LevelAction(string newGameLevel)
    {
        SceneManager.LoadScene(newGameLevel);  
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
