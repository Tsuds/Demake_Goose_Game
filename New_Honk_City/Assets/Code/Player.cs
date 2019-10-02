using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float movement_speed = 130.0f;
    [SerializeField] private AudioSource honk_sfx;

    private GameObject honk;

    private float honk_timer = 0.0f;
    private bool has_honked = false;

    private void Awake()
    {
        foreach(Transform child in GetComponentInChildren<Transform>())
        {
            if(child.name == "Honk")
            {
                honk = child.gameObject;
                honk.SetActive(false);
            }
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnHonk();
            if (honk_sfx)
            {
                honk_sfx.Play();
            }
            has_honked = true;
        }
        if(has_honked)
        {
            honk_timer += Time.deltaTime;
            if(honk_timer > 0.5f)
            {
                honk_timer = 0.0f;
                honk.SetActive(false);
                has_honked = false;
            }
        }
        if(honk.activeInHierarchy)
        {
            SpriteRenderer this_sr = GetComponent<SpriteRenderer>();
            honk.GetComponent<SpriteRenderer>().flipX = this_sr.flipX;
        }
    }
    private void FixedUpdate()
    {
        Movement();
    }

    public void OnHonk()
    {
        honk.SetActive(true);
        Debug.Log("<color=cyan>HONK!</color>");
    }

    private void Movement()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 force = rb.velocity;

        Vector3 honk_position = honk.transform.localPosition;

        if (Input.GetKey(KeyCode.W))
        {
            force.y = movement_speed * Time.fixedDeltaTime;
            force.x = 0;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            force.y = -movement_speed * Time.fixedDeltaTime;
            force.x = 0;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            force.x = -movement_speed * Time.fixedDeltaTime;
            force.y = 0;
            GetComponent<SpriteRenderer>().flipX = false;
            honk_position.x = -0.468f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            force.x = movement_speed * Time.fixedDeltaTime;
            force.y = 0;
            GetComponent<SpriteRenderer>().flipX = true;
            honk_position.x = 0.468f;
        }
        else
        {
            force = Vector2.zero;
        }
        Debug.Log(force);
        rb.velocity = force;
        honk.transform.localPosition = honk_position;
    }

}