﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject beak = null;
    [SerializeField] private GameObject hand = null;
    public ItemMaster items;
    public bool cooldown = false;

    void Start()
    {
        cooldown = false;
    }

    void Update()
    {
        if (transform.parent == true)//(items.ItemHeld == true)
        {
            if (Input.GetKey(KeyCode.A))
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                this.transform.parent = null;
                Debug.Log("Item Dropped");
                cooldown = true;
                items.ItemHeld = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Checks if an item is already held by player
        if (cooldown == false //&& transform.parent == false
            && items.ItemHeld == false && items.NPCHeld == false)
        {
            if (other.gameObject.tag == "Player")
            {
                this.transform.parent = beak.transform;
                transform.localPosition = new Vector2(0, 0);
                items.ItemHeld = true;
            }
        }
        //if NPC collides and is chasing, take item
        if(other.gameObject.tag == "NPC" && 
            other.gameObject.GetComponent<NPC_StateManager>().GetState() 
            == NPC_StateManager.State.chase)
        {
            //if Player is holding item, stun player
            if(items.ItemHeld)
            {
                Debug.Log("stun");
                FindObjectOfType<Player>().stunned = true;
            }
            NPCTakesItem(true);
        }

        //if (other.gameObject.tag == "NPC")
        //{
        //    //if (items.ItemHeld == false)
        //    //{
        //    //    this.transform.parent = null;
        //    //    this.transform.parent = hand.transform;
        //    //}
        //    //else
        //    if (items.ItemHeld)
        //    {
        //        NPCTakesItem();
        //    }
        //    //transform.localPosition = new Vector2(0, 0);
        //}
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        cooldown = false;
    }

    //Set properties so NPC is carrying item and player is not
    public void NPCTakesItem(bool pickingUp)
    {
        items.ItemHeld = false;
        items.NPCHeld = pickingUp;
        cooldown = false;
        this.transform.parent = null;

        if (pickingUp)
        {
            this.transform.parent = hand.transform;
            transform.localPosition = new Vector2(0, 0);
        }
    }
}
