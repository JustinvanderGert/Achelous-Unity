using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Target2 : MonoBehaviour
{
    Player playerScript;
    Slider slider;
    
    public Image InnerBorder;

    public enum ChargeStates { Inactive, Charging, Half, Full};
    public ChargeStates chargeState = ChargeStates.Inactive;

    public float currentCharge = 0;
    public float chargeTime = 10;
    


    // Start is called before the first frame update
    void Start()
    {
        playerScript = FindObjectOfType<Player>();
        slider = gameObject.GetComponent<Slider>();
    }

    private void OnEnable()
    {
        //Debug.Log("Reset By Enable");
        //ResetState();
        //chargeState = ChargeStates.Charging;
    }

    // Update is called once per frame
    void Update()
    {
        //Make it charge when it's active
        if (chargeState != ChargeStates.Inactive)
            chargeManager();

        //Change innerborders color based on charge level
        if (chargeState == ChargeStates.Full)
            InnerBorder.color = new Color(0, 255, 0);
        else if (chargeState == ChargeStates.Half)
            InnerBorder.color = new Color(255, 255, 0);
        else
            InnerBorder.color = new Color(0, 0, 0);

    }

    //Handle the charging system
    void chargeManager()
    {
        //Charge till the maximum
        currentCharge = Mathf.Clamp(currentCharge += Time.deltaTime, 0, chargeTime);
        slider.value = currentCharge / chargeTime * 100;

        //If charging is done stop it
        if (currentCharge >= chargeTime)
        {
            chargeState = ChargeStates.Full;
            return;
        }

        //Check if currentcharge has reached the halfway point (with some room for error)
        if (slider.value > 40 && slider.value < 60)
        {
            chargeState = ChargeStates.Half;
        }
        else if (slider.value > 60 && chargeState == ChargeStates.Half)
        {
            chargeState = ChargeStates.Charging;
        }
    }

    //Handle getting hit
    public void Hit()
    {
        //Got shot when charge is at midpoint
        if (chargeState == ChargeStates.Half)
        {
            Debug.Log("Crit");
            playerScript.moveScript.SpeedBoost(true);
            playerScript.moveScript.StartDoubleJump(true);
        }

        //Got shot when charge is full
        if (chargeState == ChargeStates.Full)
        {
            Debug.Log("Hit");
            playerScript.moveScript.SpeedBoost(false);
            playerScript.moveScript.StartDoubleJump(false);
        }

        ResetState();
    }

    //Reset slider variables
    public void ResetState()
    {
        chargeState = ChargeStates.Charging;
        currentCharge = 0;
        slider.value = currentCharge / chargeTime * 100;
    }
}
