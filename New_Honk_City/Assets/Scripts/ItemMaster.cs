using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMaster : MonoBehaviour
{

    public bool ItemHeld;
    public bool NPCHeld; 
    // Start is called before the first frame update
    void Start()
    {
        ItemHeld = false;
        NPCHeld = false;
    }

    // Update is called once per frame
    void Update()
    {
    }
}
