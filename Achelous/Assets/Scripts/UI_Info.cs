using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UI_Info : MonoBehaviour
{
    GameObject player;

    public float activeDistance;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    void Update()
    {
        if(gameObject.activeSelf)
            transform.LookAt(player.transform);


        float distance = Vector3.Distance(transform.position, player.transform.position);
        if(distance <= activeDistance)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}