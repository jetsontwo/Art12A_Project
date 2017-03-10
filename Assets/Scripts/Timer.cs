using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Timer : MonoBehaviour {

    private Text timer;
    public float count_down;
    public GameObject game_over_activate;
    private bool game_end = false;

    void Start()
    {
        timer = GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        if(!game_end)
            timer.text = (count_down - Time.time).ToString("F2") + " Seconds";
        if (count_down - Time.time <= 0 && !game_end)
        {
            StartCoroutine(game_over());
            game_end = true;
        }
    }

    IEnumerator game_over()
    {
        game_over_activate.SetActive(true);
        timer.text = "0.00 Seconds";
        while(true)
        {
            timer.color = Color.red;
            yield return new WaitForSeconds(0.5f);
            timer.color = Color.black;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
