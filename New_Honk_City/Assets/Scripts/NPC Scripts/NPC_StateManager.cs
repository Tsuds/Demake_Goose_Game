using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Basic idea is -
//in update check if state should be changed and change it
//in state scripts check if current_state is that state
//do whatever is needed for that state

public class NPC_StateManager : MonoBehaviour
{
    //How I planned to have items associated with NPCs
    //drag item into public variable
    public GameObject item;
    public Vector3 itemStartPos;
    public Vector3 startPos;
    public bool NPCHeld;

    //state enum so can check/change state value for appropriate
    //state scripts
    public enum State
    {
        idle = 0,
        alert = 1,
        flee = 2,
        chase = 3,
        returnToIdle = 4
    }
    private State currentState = State.idle;

    //max distance NPC will travel before returning to idle
    public int returnDistance = 10;

    // Start is called before the first frame update
    void Start()
    {
        itemStartPos = item.transform.position;
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //if NPCs item has been taken see if it is within their view
        //if in their line of sight set state to chase
        if (currentState != State.chase)
        {
            if (NPCHeld == false)
            {
                Vector3 heading = item.transform.position - transform.position;
                Vector3 direction = heading / heading.magnitude;

                Vector3 forward;

                if (gameObject.GetComponent<SpriteRenderer>().flipX)
                {
                    forward = transform.right;
                }
                else
                {
                    forward = -transform.right;
                }
                //if item is not where it should be 
                //and is within cone of Line of sight (angle and distance)
                if (item.transform.position != itemStartPos &&
                    Vector3.Angle(forward, direction) < 45.0f &&
                    Vector2.Distance(transform.position, item.transform.position )< 7.0f)
                {
                    SetState(State.chase);
                    Debug.Log("chase");
                    
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(currentState == State.idle || currentState == State.alert)
        {
            //if in honk radius set to alert
            //if (col.gameObject.name == "Honk Radius")
            //{
            //    SetState(State.alert);

            //    Vector3 heading = col.gameObject.transform.position - transform.position;
            //    Vector3 direction = heading / heading.magnitude;

            //    gameObject.GetComponent<NPC_Behaviours>().SetDirection(direction);

            //    gameObject.GetComponent<NPC_Behaviours>().lastKnownLocation = col.gameObject.transform.position;
            //    Debug.Log("alert");
            //}
            //if in honk collider set to flee
            /*else*/ if (col.gameObject.name == "Honk")
            {
                SetState(State.flee);

                Vector3 heading = transform.position - col.gameObject.transform.position;
                Vector3 direction = heading / heading.magnitude;

                gameObject.GetComponent<NPC_Behaviours>().SetDirection(direction);
            }
        }

        //if chasing player and collides, stun and return
        if(currentState == State.chase && col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<Player>().stunned = true;
            item.GetComponent<ItemBehaviour>().NPCTakesItem(true);
        }
    }

    public State GetState()
    {
        return currentState;
    }

    public void SetState(State newState)
    {
        if(newState == State.returnToIdle)
        {
            itemStartPos = item.transform.position;
        }
        currentState = newState;
    }
}