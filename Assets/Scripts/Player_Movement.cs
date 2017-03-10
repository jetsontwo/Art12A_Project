using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour {

    public float accel, max_vel, drag, jump, look_down_dist;
    private Rigidbody2D rb;
    private bool on_ground = true, boosting = false, edited_ps = false, boosted = false;
    private ParticleSystem ps;
    private ParticleSystem.ShapeModule ps_edit_shape;
    private float boost_timer = 0;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("Horizontal") > 0 && rb.velocity.x < max_vel)
            rb.velocity += new Vector2(accel * Time.deltaTime, 0);
        else if (Input.GetAxis("Horizontal") < 0 && rb.velocity.x > -max_vel)
            rb.velocity -= new Vector2(accel * Time.deltaTime, 0);
        else
            rb.velocity -= new Vector2(rb.velocity.x * drag * Time.deltaTime, 0);

        if (Input.GetKeyDown(KeyCode.Space) && on_ground)
            rb.velocity = new Vector2(rb.velocity.x, jump * Time.deltaTime);

        if (boosting && boost_timer == 0)
        {
            boost_timer += 0.0001f;
            StartCoroutine(engage_boost());
        }
        else if (ps != null && boost_timer == 5)
        {
            boost_timer -= 0.0001f;
            StartCoroutine(disengage_boost());
        }
        else if (ps == null) 
        {
            boost_timer = 0;
            if(boosted)
                StopCoroutine(engage_boost());
            boosted = false;
        }
    }



    void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Ground"))
            on_ground = true;
        else if (col.CompareTag("Boost"))
        {
            ps = col.GetComponent<ParticleSystem>();
            ps_edit_shape = ps.shape;
            boosting = true;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Ground"))
            on_ground = false;
        else if(col.CompareTag("Boost"))
        {
            if (edited_ps)
                clear_ps();
            ps = null;
            boosting = false;
        }
    }


    IEnumerator engage_boost()
    {
        ps_edit_shape.shapeType = ParticleSystemShapeType.Cone;
        ParticleSystem.MainModule ps_edit_size = ps.main;
        ps_edit_size.startSize = 0.5f;
        edited_ps = true;
        while(boost_timer < 5)
        {
            yield return new WaitForSeconds(0.1f);
            boost_timer += 0.1f;
        }

        accel *= 1.2f;
        max_vel *= 1.2f;
        jump *= 1.2f;
        boost_timer = 5;
        boosted = true;
        yield return new WaitForSeconds(0.1f);
    }

    IEnumerator disengage_boost()
    {
        print("disengage");
        ps_edit_shape.shapeType = ParticleSystemShapeType.Box;
        var ps_edit_size = ps.main;
        ps_edit_size.startSize = 0.1f;
        while (boost_timer > 0)
        {
            yield return new WaitForSeconds(0.1f);
            boost_timer -= 0.15f;
        }

        print("decrease");

        accel /= 1.2f;
        max_vel /= 1.2f;
        jump /= 1.2f;
        boost_timer = 0;
        ps = null;
        edited_ps = false;
        yield return new WaitForSeconds(0.1f);
    }

    private void clear_ps()
    {
        ps_edit_shape.shapeType = ParticleSystemShapeType.Box;
        var ps_edit_size = ps.main;
        ps_edit_size.startSize = 0.1f;
    }
}
