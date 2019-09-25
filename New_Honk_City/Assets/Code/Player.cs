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
		Debug.Log(context.ReadValue<float>());
        Debug.Log("<color=red>HONK!</color>");
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log("Moving");
    }
}
