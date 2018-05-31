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
    public IEnumerator ResetLevel()
    {
       
        gameOverText.gameObject.SetActive(true);
        Time.timeScale = 0.00001f;
        yield return new WaitForSecondsRealtime(1);
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(ResetLevel());
        }
        
    }
    // Update is called once per frame
    void Update () {
		
	}
}
