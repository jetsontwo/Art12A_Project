using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour {

    private Text timer;
    public float count_down;
    private float init_time;
    public GameObject game_over_activate, win_activate, boost_text;
    private bool game_end = false;

    void Start()
    {
        init_time = count_down;
        timer = GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        count_down -= Time.deltaTime;
        if(!game_end)
            timer.text = (count_down).ToString("F2") + " Seconds";
        if (count_down <= 0 && !game_end)
        {
            StartCoroutine(game_over());
            game_end = true;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            count_down = init_time;
            Time.timeScale = 1;
            SceneManager.LoadScene(1);
        }
    }

    IEnumerator game_over()
    {
        game_over_activate.SetActive(true);
        Time.timeScale = 0;
        timer.text = "0.00 Seconds";
        while(true)
        {
            timer.color = Color.red;
            yield return new WaitForSeconds(0.5f);
            timer.color = Color.black;
            yield return new WaitForSeconds(0.5f);
        }
    }


    public void win()
    {
        timer.color = Color.green;
        game_end = true;
        win_activate.SetActive(true);
    }
    
    public IEnumerator show_boost_text()
    {
        boost_text.SetActive(true);
        yield return new WaitForSeconds(2f);
        boost_text.SetActive(false);
    }
}
