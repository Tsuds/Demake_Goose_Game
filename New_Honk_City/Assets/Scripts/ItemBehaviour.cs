using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject beak = null;
    [SerializeField] private GameObject hand = null;
    public bool cooldown = false;
    public NPC_StateManager NPC;
    public Player player;
	private GameObject startParent;

    void Start()
    {
        //player = FindObjectOfType<Player>();
        cooldown = false;
		startParent = transform.parent.gameObject;
    }

    void Update()
    {
        if (transform.parent.transform != startParent.transform && NPC.NPCHeld == false)
        {
            if (Input.GetKey(KeyCode.A))
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }

            if (Input.GetKeyDown(KeyCode.Space)&& transform.parent.tag == "Player")// && player.itemHeld)
            {
                transform.parent = startParent.transform;
                Debug.Log("Item Dropped");
                cooldown = true;
                player.itemHeld = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Checks if an item is already held by player
        if (!cooldown && !player.itemHeld && !NPC.NPCHeld)
        {
            if (other.gameObject.tag == "Player")
            {
				Debug.Log("Picked up ", gameObject);
                this.transform.parent = beak.transform;
                transform.localPosition = new Vector2(0, 0);
                player.itemHeld = true;
            }
        }
        //if NPC collides and is chasing, take item
        if(other.gameObject.tag == "NPC" && 
            other.gameObject.GetComponent<NPC_StateManager>().GetState() 
            == NPC_StateManager.State.chase && other.gameObject.GetComponent<NPC_StateManager>().item == this.gameObject)
        {
            //if Player is holding item, stun player
            if(player.itemHeld)
            {
                Debug.Log("stun");
                player.stunned = true;
                this.transform.parent = null;
            }
            NPCTakesItem(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        cooldown = false;
    }

    //Set properties so NPC is carrying item and player is not
    public void NPCTakesItem(bool pickingUp)
    {
        player.itemHeld = false;
        NPC.NPCHeld = pickingUp;
        cooldown = false;
        this.transform.parent = null;

        if (pickingUp)
        {
            this.transform.parent = hand.transform;
            transform.localPosition = new Vector2(0, 0);
        }
        Vector3 pos = gameObject.transform.position;
        pos.z = -2.0f;
        gameObject.transform.position = pos;
    }
}
