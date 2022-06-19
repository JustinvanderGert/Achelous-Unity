using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FovChange : MonoBehaviour
{
    private PlayerMovement_FirstPerson player;

    public float player_speed;
    public float last_speed;
    public float currentFov; //currentQuantity
    public float desiredFov; //desiredQuantity
    public float zoomStep = 20.0f; //replaced 3.0f by 20.0f
    
    public float zoomBoundOne = 10.0f;
    public float zoomBoundTwo = 30.0f;

    public float fovBoundOne = 60f;
    public float fovBoundTwo = 70f;
    public float fovBoundThree = 80f;
    void Start()
    {
        currentFov = 50f;
        desiredFov = currentFov;
        player = (GameObject.Find("Player").GetComponent<PlayerMovement_FirstPerson>());
    }

    void CheckSpeed()
    {
        if (player_speed < zoomBoundOne) //swapped last_speed with zoomBound
        {
            //print("Player Speed Decreasing - Zoom IN!");
            last_speed = player_speed;
            desiredFov = fovBoundOne;
            //currentFOV to minFOV
        }
        else if (player_speed > zoomBoundOne && player_speed < zoomBoundTwo) //swapped last_speed with zoomBound
        {
            //print("Player Speed Increasing - Zoom OUT!");
            last_speed = player_speed;
            desiredFov = fovBoundTwo;
            //current FOV to maxFOV
        }
        else if (player_speed > zoomBoundTwo)
        {
            desiredFov = fovBoundThree;
        }

        if (player_speed < last_speed)
        {
            last_speed = player_speed;
            zoomStep = 50f;
        }
        else if (player_speed > last_speed)
        {
            last_speed = player_speed;
            zoomStep = 20f;
        }
        
    }

    void ProcessFOV()
    {
        currentFov = Mathf.MoveTowards(currentFov, desiredFov, zoomStep * Time.deltaTime);
    }

    void SetFOV()
    {
        Camera.main.fieldOfView = currentFov;
    }

    void Update()
    {
        player_speed = Math.Abs(player.speedScore);
        //player_speed = (GameObject.Find("Player").GetComponent<PlayerMovement_FirstPerson>().speedScore);

        CheckSpeed();
        ProcessFOV();
        SetFOV();

    }
}
