using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour {

    [SerializeField]
    Text healthText;


    string text = "Health: ";
    float maxHealth = 100f;
    float currentHealth;
	// Use this for initialization
	void Start () {
        currentHealth = maxHealth;
        healthText.text = text + maxHealth.ToString();
	}
	
	// Update is called once per frame
	void Update () {
        healthText.text = text + currentHealth.ToString();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Planet")
        {
            if (currentHealth > 0)
            {
                currentHealth = currentHealth - 0.005f;
                //Debug.Log("crash");

            }
            else {
                SceneManager.LoadScene("Level C1");
            }
        }

        if (collision.gameObject.tag == "rock")
        {
            if (currentHealth > 0)
            {
                currentHealth = currentHealth - 3;
                Debug.Log("crash");

            }
            else
            {
                SceneManager.LoadScene("Level C1");
            }
        }
    }
}
