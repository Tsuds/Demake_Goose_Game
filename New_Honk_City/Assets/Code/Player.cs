using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float movement_speed = 4.0f;

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
        float new_x = transform.position.x;
        float new_y = transform.position.y;

        Vector3 honk_position = honk.transform.localPosition;

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
        }
        else if (Input.GetKey(KeyCode.D))
        {
            new_x += movement_speed * Time.deltaTime;
            GetComponent<SpriteRenderer>().flipX = true;
            honk_position.x = 0.468f;
        }
        transform.position = new Vector3(new_x, new_y, transform.position.z);
        honk.transform.localPosition = honk_position;
    }

}