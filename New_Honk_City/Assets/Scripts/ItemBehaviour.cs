using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    public Items data;        
    
    public string ItemName;
    public string NPCName;

    public bool honk = false;


    // Start is called before the first frame update
    void Start()
    {
        data = GetComponent<Items>();
        honk = false;

    }

    // Update is called once per frame
    void Update()
    {
     if (honk == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                honk = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        honk = true;
    }


}
