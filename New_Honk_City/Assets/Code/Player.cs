using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float movement_speed = 4.0f;
    [SerializeField] private AudioSource honk_sfx;

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
            if (Input.GetKeyDown(KeyCode.Space))
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
        float new_x = transform.position.x;
        float new_y = transform.position.y;

        Vector3 honk_position = honk.transform.localPosition;
        Vector3 item_position = itemHolder.transform.localPosition;

        if (Input.GetKey(KeyCode.W))
        {
            new_y += movement_speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            new_y -= movement_speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            new_x -= movement_speed * Time.deltaTime;
            GetComponent<SpriteRenderer>().flipX = false;
            honk_position.x = -0.468f;
            item_position.x = -0.468f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            new_x += movement_speed * Time.deltaTime;
            GetComponent<SpriteRenderer>().flipX = true;
            honk_position.x = 0.468f;
            item_position.x = 0.468f;
        }
        transform.position = new Vector3(new_x, new_y, transform.position.z);
        honk.transform.localPosition = honk_position;
        itemHolder.transform.localPosition = item_position;
    }
}