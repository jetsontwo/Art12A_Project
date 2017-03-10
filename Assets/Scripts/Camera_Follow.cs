using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour {

    public GameObject to_follow;
    public float approach_speed, cutoff;
	
	// Update is called once per frame
	void Update () {
        Vector3 diff = new Vector3(transform.position.x - to_follow.transform.position.x, transform.position.y - to_follow.transform.position.y, 0);
        if (Mathf.Abs(transform.position.magnitude - to_follow.transform.position.magnitude) > cutoff)
            transform.position -= diff * approach_speed * Time.deltaTime;
	}
}
