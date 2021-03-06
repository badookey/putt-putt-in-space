﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class Score
{
    public string name { get; set; }
    public int shots { get; set; }
    public int deaths { get; set; }
    public float time { get; set; }
}

[System.Serializable]
public class HighScore : MonoBehaviour{

    //public List<Score> scores = new List<Score>();
    static Score current = new Score();
    //HighScore high;
	// Use this for initialization
	void Start () {
        Debug.Log("Start called");
        //high = new HighScore();
    }
    public static float[] Save(string sceneName)
    {
        //HighScore highScore = new HighScore();
        string scene = SceneManager.GetActiveScene().name;
        GameObject gameObject = GameObject.Find("Player");
        AccumulationMovement movement = gameObject.GetComponent<AccumulationMovement>();
        current.shots = movement.ValidHits;
        current.time = Time.timeSinceLevelLoad;
        current.name = sceneName;
        current.deaths = 0;
        int highshot = PlayerPrefs.GetInt("shots"+scene);
        float hightime = PlayerPrefs.GetFloat("time" + scene);
        Debug.Log("highshot = "+highshot);
        Debug.Log("hightime = "+hightime);
        Debug.Log("current shot = " + current.shots);
        Debug.Log("current time = " + current.time);
        if (highshot==0)
        {
            PlayerPrefs.SetFloat("time" + scene, current.time);
            PlayerPrefs.SetInt("shots" + scene, current.shots);
            PlayerPrefs.Save();
        }
        if (current.shots <= highshot)
        {
            if(current.time<hightime||current.shots<highshot)
            {
                Debug.Log("should be saving here");
                PlayerPrefs.SetFloat("time" + scene, current.time);
                PlayerPrefs.SetInt("shots" + scene, current.shots);
                PlayerPrefs.Save();
            }
        }
        
        
        //Convert to Json
        /*string jsonData = JsonUtility.ToJson(current);
        //Save Json string
        if (jsonData != null)
        {
            Debug.Log("saving: " + jsonData);
            PlayerPrefs.SetString("MySettings", jsonData);
            PlayerPrefs.Save();
        }*/
        float[] a = { current.shots, current.time, highshot, hightime };
        return a;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        int shot = PlayerPrefs.GetInt("shots");
        Load(shot);
        /*
        string jsonData = PlayerPrefs.GetString("MySettings");
        Debug.Log("json string :"+jsonData);
        //Convert to Class
        Score loadedData = JsonUtility.FromJson<Score>(jsonData);
        Debug.Log("scene name from save is :"+loadedData.name);
        if (scene.name == loadedData.name)
            Load(loadedData);*/
        //Debug.Log("OnSceneLoaded: " + scene.name);
        //Debug.Log(mode);
    }

    public static void Load(int loadedData)
    {
        //Load saved Json
       
        GameObject gameObject = GameObject.Find("Player");
        AccumulationMovement movement = gameObject.GetComponent<AccumulationMovement>();
        Debug.Log(loadedData);
        movement.ValidHits = loadedData;
       
        
    }
    // Update is called once per frame
    void OnEnable()
    {
        Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnDisable()
    {
        Debug.Log("OnDisable");
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
