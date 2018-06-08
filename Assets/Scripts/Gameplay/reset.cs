using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class reset : MonoBehaviour {

    public GameObject gameOverText;
    // Use this for initialization
    void Start () {
        
    }

    public IEnumerator WaitSeconds(float time)
    {
        yield return new WaitForSecondsRealtime(time);
    }
    public IEnumerator ResetLevel()
    {
        //GameObject parent = gameOverText.transform.parent.gameObject;
        //GameObject s = GameObject.FindGameObjectWithTag("score");
        gameOverText = GameObject.FindGameObjectWithTag("Deathtext");
        gameOverText.GetComponent<Text>().enabled =true;
        Time.timeScale = 0.000000000001f;
        float pauseEndTime = Time.realtimeSinceStartup ;
        pauseEndTime += (float).5;
        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            yield return 0;
        }
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(ResetLevel());
            //ResetLevel();
        }
        
    }
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown("r"))
            StartCoroutine(ResetLevel());

    }
}
