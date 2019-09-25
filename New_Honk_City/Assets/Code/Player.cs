using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, GeneralInputs.IPlayerActions
{
    private GeneralInputs controls;

    void Awake()
    {
        controls = new GeneralInputs();
        controls.Player.SetCallbacks(this);
    }

    void OnEnable()
    {
        controls.Player.Enable();
        Debug.Log("<color=green>Enabled controller</color>");
    }

    void OnDisable()
    {
        controls.Player.Disable();
        Debug.Log("<color=red>Disabled controller</color>");
    }

    public void OnHonk(InputAction.CallbackContext context)
    {
        Debug.Log("<color=cyan>HONK!</color>");
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Vector2 direction = context.ReadValue<Vector2>();

        if(direction == new Vector2(-1.0f,0.0f))
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        else if(direction == new Vector2(1.0f,0.0f))
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        transform.position += new Vector3(direction.x, direction.y, 0);
    }
}
