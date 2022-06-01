using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public GameObject selectedObj;

    public float chargeTime = 3;
    public float currentCharge = 0;
    public float despawnTime = 5;

    public bool visible = false;
    public bool startCharging = false;
    public bool fullCharge = false;
    public bool midPointCharge = false;

    Camera aimCamera;

    GameObject player;
    Player playerScript;

    bool despawning = false;
    bool isRendered = false;
    bool seenHandled = false;

    Coroutine despawnRoutine;
    

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();

        gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        aimCamera = GameObject.FindGameObjectWithTag("AimCam").GetComponent<Camera>();
    }


    void Update()
    {
        if (startCharging && !fullCharge)
        {
            chargeManager();
        }
        
        transform.parent.LookAt(player.transform, Vector3.up);

        //Only check if it is visible while its being rendered
        if (isRendered)
        {
            CheckIfSeen(aimCamera);
        }
    }


    void chargeManager()
    {
        gameObject.GetComponent<Renderer>().material.SetColor("_BaseColor", Color.red);

        
        //Charge till the maximum
        currentCharge = Mathf.Clamp(currentCharge += Time.deltaTime, 0, chargeTime);
        

        //If charging is done stop it
        if (currentCharge >= chargeTime)
        {
            //Debug.Log("Fully charged");
            GetComponent<Renderer>().material.SetColor("_BaseColor", Color.green);
            startCharging = false;
            fullCharge = true;
            return;
        }

        //Check if currentcharge has reached the halfway point (with some room for error
        if (currentCharge < (chargeTime * 0.6) && currentCharge > (chargeTime * 0.4))
        {
            //yellow
            //Debug.Log("MidPoint charged");

            midPointCharge = true;
            GetComponent<Renderer>().material.SetColor("_BaseColor", Color.yellow);

        }
        else if (currentCharge > (chargeTime * 0.6) && midPointCharge)
        {
            //red
            //Debug.Log("Midpoint passed");

            GetComponent<Renderer>().material.SetColor("_BaseColor", Color.red);
            midPointCharge = false;
        }
    }


    //Checks if object collider is within the view of the camera;
    void CheckIfSeen(Camera aimCamera)
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(aimCamera);
        Collider objCollider = GetComponent<Collider>();

        if (GeometryUtility.TestPlanesAABB(planes, objCollider.bounds))
            IsSeen();
        else
            IsNotSeen();
    }


    void IsSeen()
    {
        if(seenHandled == false)
        {
            playerScript.visibleTargets.Add(gameObject);

            //Cancel despawning if it was activated
            if (despawning && despawnRoutine != null)
            {
                StopCoroutine(despawnRoutine);
                despawning = false;
            }
        }
        seenHandled = true;
    }

    void IsNotSeen()
    {
        if (seenHandled == true)
        {
            //Start despawning timer
            if (gameObject.activeSelf)
            {
                despawnRoutine = StartCoroutine(DespawnTimer());
            }

            playerScript.visibleTargets.Remove(gameObject);
        }
        gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        isTarget(false);
        seenHandled = false;
    }


    private void OnBecameVisible()
    {
        isRendered = true;
    }

    private void OnBecameInvisible()
    {
        isRendered = false;
    }


    IEnumerator DespawnTimer()
    {
        /*
        //Debug.Log("Start despawning");
        despawning = true;

        //Despawn after given time

        playerScript.visibleTargets.Remove(gameObject);
        Destroy(gameObject);*/
        yield return new WaitForSeconds(despawnTime);
    }

    public void isTarget(bool isIt)
    {
        selectedObj.SetActive(isIt);
    }

    public void Hit()
    {
        playerScript.visibleTargets.Remove(gameObject);

        //Got shot when charge is at midpoint
        if(midPointCharge)
        {
            Debug.Log("Crit");
            playerScript.moveScript.SpeedBoost(true);
            playerScript.moveScript.StartDoubleJump(true);
        } 
        
        //Got shot when charge is full
        if (fullCharge)
        {
            //Debug.Log("Hit");
            playerScript.moveScript.SpeedBoost(false);
            playerScript.moveScript.StartDoubleJump(false);
        }

        Destroy(transform.parent.gameObject);
    }
}