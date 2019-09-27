using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    public bool honk = false;
    public bool cooldown = false;
    public float time;

    void Start()
    {        
        honk = false;
        cooldown = false;
    }

    void Update()
    {
     if (honk == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                this.transform.parent = null;
                time = 1.5f;
                CountDown();
                honk = false;
            }
        }
        timeCheck();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (cooldown == false)
        {
            if (other.gameObject.tag == "Player")
            {            
                honk = true;
                this.transform.parent = other.transform;
            }
        }
    }

    void CountDown()
    {
        time -= Time.deltaTime;
        cooldown = true;
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
//http://docs.unity3d.com/Documentation/ScriptReference/Collider.OnTriggerEnter.html?_ga=2.99166366.1045234588.1569588832-344143463.1569588832
}
