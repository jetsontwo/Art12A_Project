using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colored_Show : MonoBehaviour {

    private bool going_up = false;
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (going_up)
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a + 0.01f);
        else
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, sr.color.a - 0.01f);

        if (sr.color.a <= 0)
            going_up = true;
        else if (sr.color.a >= 0.2f)
            going_up = false;
    }
}
