using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class reset : MonoBehaviour {

    public Text gameOverText;
    // Use this for initialization
    void Start () {
    }

    public IEnumerator WaitSeconds(float time)
    {
        yield return new WaitForSecondsRealtime(time);
    }
    public string ResetLevel()
    {
        //GameObject parent = gameOverText.transform.parent.gameObject;
        //GameObject s = GameObject.FindGameObjectWithTag("score");
        HighScore.Save(SceneManager.GetActiveScene().name);
        gameOverText.gameObject.SetActive(true);
        Time.timeScale = 0.00001f;
        WaitSeconds(1);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
        return null;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //StartCoroutine(ResetLevel());
            ResetLevel();
        }
        
    }
    // Update is called once per frame
    void Update () {
		
	}
}
