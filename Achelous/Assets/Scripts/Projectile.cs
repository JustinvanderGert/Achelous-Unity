using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public Vector3 targetPos;

    void Start()
    {

    }
    
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
    }

    public void SetTarget(GameObject newTarget)
    {
        targetPos = newTarget.transform.position;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Shootables>())
        {
            other.gameObject.GetComponent<Shootables>().Hit();
            Destroy(other.gameObject);
            Destroy(transform.parent.gameObject);
        }
    }
}