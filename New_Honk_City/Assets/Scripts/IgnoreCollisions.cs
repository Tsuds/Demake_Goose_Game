using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollisions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag != "NPC")
        {
            Debug.Log(col.gameObject.tag);
            Physics2D.IgnoreCollision(col.collider, gameObject.GetComponent<CircleCollider2D>());
        }
    }
}
