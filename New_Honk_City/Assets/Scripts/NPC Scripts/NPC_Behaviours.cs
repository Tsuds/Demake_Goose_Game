﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Behaviours : MonoBehaviour
{
    NPC_StateManager stateManager;

    private Vector3 direction;
    private float timer = 1.0f;

    public float speed;

    public Transform[] points;
    private int destPoint = 0;
    private int newDest;
    private bool newPoint = false;

    private float waitTime;
    public float startWaitTime;

    void Start()
    {
        destPoint = Random.Range(0, points.Length);
        stateManager = gameObject.GetComponent<NPC_StateManager>();
    }

    void Update()
    {
        switch(stateManager.GetState())
        {
            case NPC_StateManager.State.idle:
                {
                    IdleBehaviour();
                    break;
                }
            case NPC_StateManager.State.alert:
                {
                    AlertBehaviour();
                    break;
                }
            case NPC_StateManager.State.flee:
                {
                    FleeBehaviour();
                    break;
                }
            case NPC_StateManager.State.chase:
                {
                    ChaseBehaviour();
                    break;
                }
            case NPC_StateManager.State.returnToIdle:
                {
                    ReturnToIdleBehaviour();
                    break;
                }
        }
    }

    void IdleBehaviour()
    {
        transform.position = Vector2.MoveTowards(transform.position, points[destPoint].position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, points[destPoint].position) < 0.2f)
        {
            newPoint = false;
            if (waitTime <= 0)
            {
                while (!newPoint)
                {
                    newDest = Random.Range(0, points.Length);
                    if (newDest != destPoint)
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

    void AlertBehaviour()
    {

    }

    void FleeBehaviour()
    {
        transform.position += direction * 2.0f * Time.deltaTime;

        timer -= Time.deltaTime;

        if(timer <= 0.0f)
        {
            timer = 1.5f;
            stateManager.SetState(NPC_StateManager.State.idle);
        }
    }

    void ChaseBehaviour()
    {

    }

    void ReturnToIdleBehaviour()
    {

    }

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection;
    }
}