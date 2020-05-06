using UnityEngine;

public class NPC_Behaviours : MonoBehaviour
{
    NPC_StateManager stateManager;

    private Vector3 direction;
    private float timer = 1.0f;
    private float alertTimer = 2.0f;

    public Vector3 lastKnownLocation;

    public float speed;

    public bool randomPatrol;
    public Transform[] points;
    private int destPoint = 0;
    private int newDest;
    private bool newPoint = false;

    public float startWaitTime;
    private float waitTime;

    void Start()
    {
        //destPoint = Random.Range(0, points.Length);
        stateManager = gameObject.GetComponent<NPC_StateManager>();
        waitTime = startWaitTime;
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
        transform.position = Vector2.MoveTowards(transform.position, 
            points[destPoint].position, speed * Time.deltaTime);

        Vector3 heading = points[destPoint].position - transform.position;
        direction = heading / heading.magnitude;

        if (Vector2.Distance(transform.position, points[destPoint].position) < 0.2f)
        {
            
            if (waitTime <= 0)
            {
                if (randomPatrol)
                {
                    newPoint = false;
                    while (!newPoint)
                    {
                        newDest = Random.Range(0, points.Length);
                        if (newDest != destPoint)
                        {
                            destPoint = newDest;
                            newPoint = true;
                        }
                    }
                }
                else
                {
                    destPoint++;
                    if (destPoint == points.Length)
                    {
                        destPoint = 0;
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
        alertTimer -= Time.deltaTime;

        if(alertTimer <= 0.0f)
        {
            alertTimer = 2.0f;
            stateManager.SetState(NPC_StateManager.State.returnToIdle);
        }
    }

    //move in the flee direction (away from player) for set time
    void FleeBehaviour()
    {
        transform.position += direction * 2.0f * Time.deltaTime;

        timer -= Time.deltaTime;

        if(timer <= 0.0f)
        {
            timer = 1.0f;
            stateManager.SetState(NPC_StateManager.State.idle);
        }
    }

    //run towards item and return it to it's origin
    void ChaseBehaviour()
    {        
        transform.position = Vector2.MoveTowards(transform.position,
            stateManager.item.transform.position, 
            speed * Time.deltaTime);

        Vector3 heading = stateManager.item.transform.position - transform.position;
        direction = heading / heading.magnitude;

        //once NPC is holding the item, return it to it's origin, drop, return to idle
        if (stateManager.NPCHeld)
        {
            transform.position = Vector2.MoveTowards(transform.position,
            stateManager.itemStartPos, speed * Time.deltaTime);

            heading = stateManager.itemStartPos - transform.position;
            direction = heading / heading.magnitude;

            if (Vector2.Distance(transform.position, stateManager.itemStartPos) < 0.2f)
            {
                stateManager.item.GetComponent<ItemBehaviour>().NPCTakesItem(false);
                stateManager.SetState(NPC_StateManager.State.returnToIdle);
            }
        }
    }

    //return to origin point and change state to idle
    void ReturnToIdleBehaviour()
    {
        transform.position = Vector2.MoveTowards(transform.position,
           stateManager.startPos, speed * Time.deltaTime);

        Vector3 heading = stateManager.startPos - transform.position;
        direction = heading / heading.magnitude;

        if (Vector2.Distance(transform.position, stateManager.startPos) < 0.2f)
        {
            stateManager.SetState(NPC_StateManager.State.idle);
        }
    }

    //sets direction NPC is moving to/facing
    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection;
    }

    //flips the NPC to face the correct direction
    private void LateUpdate()
    {
        if(direction.x > 0 && !gameObject.GetComponent<SpriteRenderer>().flipX)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (direction.x < 0 && gameObject.GetComponent<SpriteRenderer>().flipX)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
    }
}