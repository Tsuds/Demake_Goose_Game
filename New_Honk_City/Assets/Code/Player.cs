using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float movement_speed = 130.0f;
    [SerializeField] private AudioSource honk_sfx;

    enum eDirection { NONE = 0, HORIZONTAL = 1, VERTICAL = 2 }

    private eDirection dir = eDirection.NONE;

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
        Movement();

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

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        float speed = movement_speed * Time.deltaTime;

        if (horizontal != 0)
        {
            dir = eDirection.HORIZONTAL;
 
            if (horizontal < 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
                honk_position.x = -0.468f;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = true;
                honk_position.x = 0.468f;
            }
        }
        else if (vertical != 0)
        {
            dir = eDirection.VERTICAL;
        }

        switch (dir)
        {
            case eDirection.VERTICAL:
            {
                force = new Vector2(0, vertical * speed);
                break;
            }
            case eDirection.HORIZONTAL:
            {
                force = new Vector2(horizontal * speed, 0);
                break;
            }
            default:
            {
                force = Vector2.zero;
                break;
            }
        }

        rb.velocity = force;
        honk.transform.localPosition = honk_position;
    }

}