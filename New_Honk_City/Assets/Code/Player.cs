using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float movement_speed = 140.0f;
    [SerializeField] private AudioSource honk_sfx;

    private enum direction { NONE = 0, VERTICAL = 1, HORIZONTAL = 2, DIAGONAL = 3 }

    private direction dir = direction.NONE;

    private GameObject honk;
    public GameObject itemHolder;

    private float honk_timer = 0.0f;
    private bool has_honked = false;

    public bool itemHeld = false;

    private float stunTimer = 1.5f;
    public bool stunned = false;

    private GameObject stunEffect;

    private void Awake()
    {
        foreach(Transform child in GetComponentInChildren<Transform>())
        {
            if(child.name == "Honk")
            {
                honk = child.gameObject;
                honk.SetActive(false);
            }
            else if (child.name == "StunEffect")
            {
                stunEffect = child.gameObject;
                stunEffect.SetActive(false);
            }
        }
    }
    private void Update()
    {        
        if (stunned)
        {
            if(!stunEffect.activeSelf)
            {
                if (GetComponent<SpriteRenderer>().flipX)
                {
                    stunEffect.transform.localPosition = new Vector2(0.115f,0.5f);
                }
                else
                {
                    stunEffect.transform.localPosition = new Vector2(-0.115f, 0.5f);
                }
                stunEffect.SetActive(true);
            }
            stunTimer -= Time.deltaTime;
            if (stunTimer <= 0.0f)
            {
                stunned = false;
                stunTimer = 1.5f;
                stunEffect.SetActive(false);
            }
        }
        else
        {
            Movement();
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.JoystickButton0))
            {
                OnHonk();
                if (honk_sfx)
                {
                    honk_sfx.Play();
                }
                has_honked = true;
            }
            if (has_honked)
            {
                honk_timer += Time.deltaTime;
                if (honk_timer > 0.5f)
                {
                    honk_timer = 0.0f;
                    honk.SetActive(false);
                    has_honked = false;
                }
            }
            if (honk.activeInHierarchy)
            {
                SpriteRenderer this_sr = GetComponent<SpriteRenderer>();
                honk.GetComponent<SpriteRenderer>().flipX = this_sr.flipX;
            }
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
        Vector3 item_position = itemHolder.transform.localPosition;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        float speed = movement_speed * Time.deltaTime;

        if (horizontal != 0)
        {
            if (horizontal < 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
                honk_position.x = -0.468f;
                item_position.x = -0.468f;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = true;
                honk_position.x = 0.468f;
                item_position.x = 0.468f;
            }
            if (vertical == 0)
            {
                dir = direction.HORIZONTAL;
            }
            else
            {
                dir = direction.DIAGONAL;
            }
        }
        else if(vertical != 0)
        {
            dir = direction.VERTICAL;
        }

        if(horizontal == 0 && vertical == 0)
        {
            dir = direction.NONE;
        }        

        switch(dir)
        {
            case direction.HORIZONTAL:
            {
                force = new Vector2(horizontal * speed, 0);
                break;
            }
            case direction.VERTICAL:
            {
                force = new Vector2(0, vertical * speed);
                break;
            }
            case direction.DIAGONAL:
            {
                force = new Vector2(horizontal * speed, vertical * speed);
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
        itemHolder.transform.localPosition = item_position;
    }
}