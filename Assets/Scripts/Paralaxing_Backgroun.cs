using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralaxing_Backgroun : MonoBehaviour {

    private float left = -20, right = 200, speed;

    private SpriteRenderer sr;

    void Start()
    {
        speed = Random.Range(3f, 5f);
        sr = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
        if (transform.position.x <= left)
        {
            transform.position = new Vector2(right, Random.Range(7, 15));
            float grayness = Random.Range(.65f, 1f);
            sr.color = new Color(grayness, grayness, grayness, Random.Range(.28f, .7f));
            transform.localScale = new Vector3(Random.Range(5, 10), Random.Range(2, 5));
            speed = Random.Range(3, 5);
        }
        transform.Translate(new Vector3(-speed * Time.deltaTime, 0));
	}
}
