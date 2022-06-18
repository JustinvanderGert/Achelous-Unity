using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishBehaviour : MonoBehaviour
{
    public float speed = 10;
    public float swimDistance = 20;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void GetTargetPosition()
    {
        Vector3 targetPosition = Random.insideUnitSphere * Random.Range(10, swimDistance);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, (targetPosition - transform.position).normalized, out hit, swimDistance + 5))
        {
            GetTargetPosition();
        }
        else
        {

        }
    }
}