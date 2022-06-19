using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBehaviour : MonoBehaviour
{
    public float speed = 10;
    public float rotateSpeed = 10;
    public float swimDistance = 20;
    public float newPointDist = 5;

    Vector3 targetPosition;
    bool foundNewPos = false;
    bool gettingNewPos = false;


    void Update()
    {
        if (foundNewPos == true)
        {
            //transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);

            Vector3 newDirection = Vector3.RotateTowards(transform.forward, (targetPosition - transform.position), rotateSpeed * Time.deltaTime, 0);
            transform.rotation = Quaternion.LookRotation(newDirection);

        } else if (!foundNewPos && !gettingNewPos)
            GetTargetPosition();

        float distToTarget = Vector3.Distance(transform.position, targetPosition);
        if(distToTarget <= newPointDist)
        {
            foundNewPos = false;
        }
    }

    void GetTargetPosition()
    {
        gettingNewPos = true;
        targetPosition = Random.insideUnitSphere * Random.Range(5, 5 + swimDistance) + transform.position;

        if (Physics.Raycast(transform.position, Vector3.Normalize(targetPosition - transform.position), swimDistance + 5))
        {
            Debug.DrawLine(transform.position, targetPosition, Color.red, 2, false);
            gettingNewPos = false;
        }
        else
        {
            Debug.DrawLine(transform.position, targetPosition, Color.green, 2, false);
            gettingNewPos = false;
            foundNewPos = true;
        }
    }
}