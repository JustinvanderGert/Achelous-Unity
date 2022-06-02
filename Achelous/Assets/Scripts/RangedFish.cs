using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RangedFish : MonoBehaviour
{
    // Speed of the object moving, what the enemy is targetting and what the enemy is looking at
    public float speed = 1.0f;
    public Transform target;
    public float lookRadius = 15f;
    public float stopRadius = 5f;

    public GameObject projectile;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {


        float distance = Vector3.Distance(target.position, transform.position);

        if (distance <= lookRadius && distance > stopRadius)
        {
            //look towards the player
            transform.LookAt(target);
            var step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.position, step);

            //move towards player
            if (Vector3.Distance(transform.position, target.position) < 0.01f)
                target.position *= -1.0f;
        }

        else if(distance <= lookRadius && distance > stopRadius)
        {
            Instantiate(projectile);
        }


    }

    //draw radius in editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, stopRadius);
    }
}
