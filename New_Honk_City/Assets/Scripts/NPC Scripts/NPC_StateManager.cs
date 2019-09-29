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
    private int returnDistance = 5;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if NPCs item has been taken see if it is within their view
        //if in their line of sight set state to chase
        if(item.GetComponent<ItemBehaviour>().items.ItemHeld)
        {
            Vector3 heading = item.transform.position - transform.position;
            Vector3 direction = heading / heading.magnitude;

            if (Vector3.Angle(transform.right, direction) > 45.0f)
            {
                SetState(State.chase);
                Debug.Log("chase");
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //if the honk's hitbox is colliding set state to flee
        if(col.gameObject.name == "Honk" && currentState == State.idle ||
            currentState == State.alert)
        {
            SetState(State.flee);
            Debug.Log("flee");

            Vector3 heading = transform.position - col.gameObject.transform.position;
            Vector3 direction = heading / heading.magnitude;

            gameObject.GetComponent<NPC_Behaviours>().SetDirection(direction);
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
}