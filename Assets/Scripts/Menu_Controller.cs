using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Controller : MonoBehaviour {

    
	public void Change_Scenes()
    {
        SceneManager.LoadScene(1);
    }
}
