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
    public Sprite item;

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
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.name == "Honk")
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