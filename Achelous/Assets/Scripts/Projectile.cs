using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public Vector3 targetPos;
    bool targetReached = false;

    void Start()
    {

    }
    
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

        float distance = Vector3.Distance(transform.position, targetPos);
        if(distance == 0 && !targetReached)
        {
            targetReached = true;
            StartCoroutine(TimeToDespawn());
        }
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

    IEnumerator TimeToDespawn()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(transform.parent.gameObject);
    }
}