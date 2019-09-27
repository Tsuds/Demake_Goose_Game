using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    public bool honk = false;
    public bool cooldown = false;
    [SerializeField] private GameObject beak = null;
    [SerializeField] private GameObject hand = null;

    
    //public Player player = null;

    void Start()
    {        
        honk = false;
        cooldown = false;
        //player = GetComponent<Player>();
    }

    void Update()
    {
        if (honk == true)
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
                honk = false;
                cooldown = true;
            }
        }        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (cooldown == false) // && player.itemHeld == false)
        {
            if (other.gameObject.tag == "Player")
            {
                honk = true;
                this.transform.parent = beak.transform;
                transform.localPosition = new Vector2(0,0);
            }
            
        }
        if (other.gameObject.tag == "NPC")
        {
            if (honk == true)
            {
                this.transform.parent = null;
                this.transform.parent = hand.transform;            
            }
            else
            {
                this.transform.parent = hand.transform;
                honk = false;
                cooldown = false;
            }
            transform.localPosition = new Vector2(0, 0);
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        cooldown = false;
    }

    //http://docs.unity3d.com/Documentation/ScriptReference/Collider.OnTriggerEnter.html?_ga=2.99166366.1045234588.1569588832-344143463.1569588832
}
