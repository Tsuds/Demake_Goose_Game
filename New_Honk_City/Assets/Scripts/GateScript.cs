using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateScript : MonoBehaviour
{

    public bool Opened;


    // Start is called before the first frame update
    void Start()
    {
        Opened = false;
    }
    
    // Use an invisible sprite as the rotation and then a solid sprite as the gate
    // and set the parent as a trigger. Change the tag of the object that is used 
    // as a key to Key
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Key" && Opened == false)
        {
            Debug.Log("Open");
            transform.Rotate(Vector3.forward * -90);
            Opened = true;
            other.gameObject.SetActive(false);
        }
    }
}
