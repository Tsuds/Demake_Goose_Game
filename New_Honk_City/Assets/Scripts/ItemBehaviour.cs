using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    public bool honk = false;
    public bool cooldown = false;
    public float time = 50;

    // Start is called before the first frame update
    void Start()
    {        
        honk = false;
        cooldown = false;
    }

    // Update is called once per frame
    void Update()
    {
     if (honk == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                this.transform.parent = null;
                time = 50;
                CountDown();
                honk = false;
            }
        }
        timeCheck();
    }

    void OnTriggerEnter(Collider other)
    {
        if (cooldown == false)
        {
            honk = true;
            this.transform.parent = other.transform;
        }
    }

    void CountDown()
    {     
        time -= Time.deltaTime;
    }

    void timeCheck()
    {
        if (time >= 0)
        {
            cooldown = false;
        }
        else if (time <= 0)
        {
            cooldown = true;
        }
    }

}
