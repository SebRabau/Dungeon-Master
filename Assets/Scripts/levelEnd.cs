using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class levelEnd : MonoBehaviour {

    private AudioSource audioSource;
    private int count;

    public AudioClip win; //Audio

    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
        count = 0;
    }
	
	// Update is called once per frame
	void Update () {		
	}

    void OnTriggerEnter2D(Collider2D Object)
    {
        if (Object.gameObject.CompareTag("Exit") || Object.gameObject.CompareTag("Background") && count == 0)
        {
            audioSource.Stop();
            audioSource.PlayOneShot(win, 1f);
            count++;
        }
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene("Level3");
    }

    public void LoadLevelStart()
    {
        SceneManager.LoadScene("Level2");
    }
}
