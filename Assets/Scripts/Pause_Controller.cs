using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause_Controller : MonoBehaviour {

    public GameObject pause_panel;
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1)
                Time.timeScale = 0;
            else
                Time.timeScale = 1;
            pause_panel.SetActive(!pause_panel.activeSelf);
        }
	}

    public void quit()
    {
        Application.Quit();
    }
}
