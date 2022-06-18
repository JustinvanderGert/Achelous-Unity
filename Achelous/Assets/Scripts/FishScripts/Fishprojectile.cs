using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishprojectile : MonoBehaviour
{
    Transform targetPosition;
    bool targetReached = false;

    public float speed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, targetPosition.position);

        if (distance > 1 && !targetReached)
            transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, speed);

        if(distance <= 1)
        {
            StartCoroutine(ShootTimer());
        }
    }

    IEnumerator ShootTimer()
    {
        yield return new WaitForSeconds(4);
        Destroy(gameObject);

    }
}
