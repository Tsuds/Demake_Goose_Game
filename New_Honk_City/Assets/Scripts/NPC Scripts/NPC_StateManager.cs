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
    public GameObject honk;
    public Vector3 itemStartPos;
    public Vector3 startPos;
    public bool NPCHeld;
    public Animator anim;

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

    // Start is called before the first frame update
    void Start()
    {
        itemStartPos = item.transform.position;
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = gameObject.transform.position;
        pos.z = -2.0f;
        gameObject.transform.position = pos;

        CheckIfStill();

        if(honk.activeSelf)
        {
            if (currentState == State.idle || currentState == State.alert)
            {
                //if in honk radius set to alert
                if (currentState != State.alert && 
                    Vector2.Distance(transform.position, honk.transform.position) <= 2.5f &&
                    Vector2.Distance(transform.position, honk.transform.position) > 0.5f)
                {
                    SetState(State.alert);
                    SetAnimBool(State.alert);

                    Vector3 heading = honk.transform.position - transform.position;
                    Vector3 direction = heading / heading.magnitude;

                    gameObject.GetComponent<NPC_Behaviours>().SetDirection(direction);

                    gameObject.GetComponent<NPC_Behaviours>().lastKnownLocation = honk.transform.position;
                }
                //if in honk collider set to flee
                else if (Vector2.Distance(transform.position, honk.transform.position) <= 0.5f)
                {
                    SetState(State.flee);
                    SetAnimBool(State.flee);

                    Vector3 heading = transform.position - honk.transform.position;
                    Vector3 direction = heading / heading.magnitude;

                    gameObject.GetComponent<NPC_Behaviours>().SetDirection(direction);
                }
            }
        }

        //if NPCs item has been taken see if it is within their view
        //if in their line of sight set state to chase
        if (currentState != State.chase && currentState != State.alert)
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
                if (Vector2.Distance(item.transform.position, itemStartPos) > 1 &&
                    Vector3.Angle(forward, direction) < 45.0f &&
                    Vector2.Distance(transform.position, item.transform.position )< 1.8f)
                {
                    SetState(State.chase);
                    SetAnimBool(State.chase);                
                }
            }
        }
        else
        {
            if (NPCHeld == false)
            {
                Vector2 fromPosition = transform.position;
                Vector2 toPosition = FindObjectOfType<Player>().gameObject.transform.position;
                Vector2 direction = toPosition - fromPosition;

                RaycastHit2D hit = Physics2D.Linecast(fromPosition, toPosition);

                if (hit.collider.gameObject.tag != "Player" && hit.collider.gameObject.tag != "Item"
                    && hit.collider.gameObject.tag != "Honk")
                {
                    //Debug.Log("HIT: " + hit.collider.gameObject.name);
                    SetState(State.alert);
                    SetAnimBool(State.alert);
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //if chasing player and collides, stun and return
        if(currentState == State.chase && col.gameObject.tag == "Player" &&
            col.gameObject.GetComponent<Player>().itemHeld)
        {
            col.gameObject.GetComponent<Player>().stunned = true;
            item.GetComponent<ItemBehaviour>().NPCTakesItem(true);
        }
        else if(currentState == State.chase && col.gameObject.tag != "Player" &&
             NPCHeld)
        {
            Physics2D.IgnoreCollision(col.collider, gameObject.GetComponent<BoxCollider2D>());
        }

        if(col.gameObject.tag != "Player" && !NPCHeld)
        {
            Physics2D.IgnoreCollision(col.collider, gameObject.GetComponent<BoxCollider2D>(), false);
        }

    }

    public State GetState()
    {
        return currentState;
    }

    public void SetState(State newState)
    {
        currentState = newState;
    }

    void SetAnimBool(State boolState)
    {
        anim.SetBool("Idle", false);
        anim.SetBool("Running", false);
        anim.SetBool("Alert", false);
        anim.SetBool("Fleeing", false);

        switch (boolState)
        {
            case State.idle:
            {
                anim.SetBool("Idle", true);
                break;
            }

            case State.chase:
            {
               anim.SetBool("Running", true);
               break;
            }

            case State.alert:
            {
                anim.SetBool("Alert", true);
                break;
            }

            case State.returnToIdle:
            {
                anim.SetBool("Idle", true);
                break;
            }

            case State.flee:
            {
                anim.SetBool("Fleeing", true);
                break;
            }
        }
    }

    void CheckIfStill()
    {
        if (GetComponent<Rigidbody2D>().IsSleeping() && currentState != State.alert)
        {
            SetAnimBool(State.idle);
        }

        else if (currentState != State.alert && currentState != State.flee)
        {
            SetAnimBool(State.chase);
        }
    }
}