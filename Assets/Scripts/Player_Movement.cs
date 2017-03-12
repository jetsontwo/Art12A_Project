using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour {

    public float accel, max_vel, drag, jump, look_down_dist;
    private Rigidbody2D rb;
    private bool on_ground = true, boosting = false, edited_ps = false, boosted = false, won  = false;
    private ParticleSystem ps;
    private ParticleSystem.ShapeModule ps_edit_shape;
    private float boost_timer = 0;
    private GameObject checkPoint = null;
    public Timer timer;
    private AudioSource audio_cont;
    public AudioClip jump_sound, nyan;
    public GameObject[] circles;

    // Use this for initialization
    void Start () {
        audio_cont = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (max_vel > 6)
            max_vel = 6;
        else if (max_vel < 5)
            max_vel = 5;
        if (accel > 18)
            accel = 18;
        else if (accel < 15)
            accel = 15;
        if (jump > 600)
            jump = 600;
        else if (jump < 500)
            jump = 500;
        if (Input.GetAxis("Horizontal") > 0 && rb.velocity.x < max_vel)
            rb.velocity += new Vector2(accel * Time.deltaTime, 0);
        else if (Input.GetAxis("Horizontal") < 0 && rb.velocity.x > -max_vel)
            rb.velocity -= new Vector2(accel * Time.deltaTime, 0);
        else
            rb.velocity -= new Vector2(rb.velocity.x * drag * Time.deltaTime, 0);

        if (Input.GetKeyDown(KeyCode.Space) && on_ground)
        {
            audio_cont.clip = jump_sound;
            audio_cont.Play();
            rb.velocity = new Vector2(rb.velocity.x, jump * Time.deltaTime);
            
        }

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
            turn_off_circles();
            if (audio_cont.isPlaying)
                audio_cont.Stop();
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
            checkPoint = col.gameObject;
        }
        else if (col.CompareTag("Kill_Zone"))
            kill();
        else if (col.CompareTag("Finish") && !won)
            win();
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

    void win()
    {
        turn_on_circles();
        audio_cont.clip = nyan;
        audio_cont.Play();
        won = true;
        timer.win();
    }


    void kill()
    {
        if (checkPoint != null)
            transform.position = new Vector2(checkPoint.transform.position.x-4, 1);
        else
            transform.position = new Vector2(5, 1);
    }

    IEnumerator engage_boost()
    {
        audio_cont.clip = nyan;
        audio_cont.Play();
        ps_edit_shape.shapeType = ParticleSystemShapeType.Cone;
        edited_ps = true;
        turn_on_circles();
        while(boost_timer < 5)
        {
            yield return new WaitForSeconds(0.1f);
            boost_timer += 0.1f;
        }

        StartCoroutine(timer.show_boost_text());


        accel *= 1.2f;
        max_vel *= 1.2f;
        jump *= 1.2f;
        boost_timer = 5;
        boosted = true;
        
        yield return new WaitForSeconds(0.1f);
    }

    IEnumerator disengage_boost()
    {
        ps_edit_shape.shapeType = ParticleSystemShapeType.Box;
        while (boost_timer > 0)
        {
            yield return new WaitForSeconds(0.1f);
            boost_timer -= 0.15f;
        }

        turn_off_circles();
        if (audio_cont.isPlaying)
            audio_cont.Stop();
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
    }


    private void turn_on_circles()
    {
        foreach (GameObject obj in circles)
            obj.SetActive(true);
    }

    private void turn_off_circles()
    {
        foreach(GameObject obj in circles)
            obj.SetActive(false);
    }
}
