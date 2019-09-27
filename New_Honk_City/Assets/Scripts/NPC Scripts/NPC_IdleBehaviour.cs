using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class NPC_IdleBehaviour : MonoBehaviour
{

    public float speed;

    public Transform[] points;
    private int destPoint = 0;
    private int newDest;
    private bool newPoint = false;

    private float waitTime;
    public float startWaitTime;

    // Start is called before the first frame update
    void Start()
    {
        destPoint = Random.Range(0, points.Length);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, points[destPoint].position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, points[destPoint].position) < 0.2f)
        {
            if (waitTime <= 0)
            {
                while (!newPoint)
                {
                    newDest = Random.Range(0, points.Length);
                    if(newDest != destPoint)
                    {
                        destPoint = newDest;
                        newPoint = true;
                    }
                }

                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }

    }
}
