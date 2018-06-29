﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayLevel1Pressed()
    {
        SceneManager.LoadScene("Level1");
    }

    public void PlayLevel2Pressed()
    {
        SceneManager.LoadScene("Level2");
    }

    public void ExitPressed()
    {
        Application.Quit();
    }
}
