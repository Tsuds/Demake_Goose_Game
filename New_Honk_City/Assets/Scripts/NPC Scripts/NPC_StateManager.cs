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
    public state current_state = state.idle;
    public enum state
    {
        idle = 0,
        alert = 1,
        aggro = 2,
        return_to_idle = 3
    }

    //max distance NPC will travel before returning to idle
    private int return_distance = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
