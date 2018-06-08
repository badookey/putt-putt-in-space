using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class singleton : MonoBehaviour {

    private static singleton instance = null;
    private float volume;
    AudioSource m_MyAudioSource;
    public static singleton Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
            return;
        } else {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }
    // Use this for initialization
    void Start () {
        m_MyAudioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        volume = PlayerPrefs.GetFloat("volume");
        m_MyAudioSource.volume = volume;
    }
}
