using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpeedBarScript : MonoBehaviour
{
    private PlayerMovement_FirstPerson player;
    public float playerSpeedForBar;
    private Image speedFill;

    // Start is called before the first frame update
    void Start()
    {
        speedFill = GetComponent<Image>();
        player = (GameObject.Find("Player").GetComponent<PlayerMovement_FirstPerson>());
    }

    // Update is called once per frame
    void Update()
    {
        playerSpeedForBar = Math.Abs(player.speedScore) / 250f;
        //playerSpeedForBar = (GameObject.Find("Player").GetComponent<PlayerMovement_FirstPerson>().speedScore) / 100f;
        speedFill.fillAmount = playerSpeedForBar;
    }
}
