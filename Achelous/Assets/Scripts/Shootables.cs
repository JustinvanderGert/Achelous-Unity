using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootables : MonoBehaviour
{
    Player playerScript;
    Target2 target;
    Camera aimCamera;

    bool isRendered = false;
    bool seenHandled = false;


    void Start()
    {
        playerScript = FindObjectOfType<Player>();
        target = playerScript.target.GetComponent<Target2>();

        aimCamera = GameObject.FindGameObjectWithTag("AimCam").GetComponent<Camera>();
    }

    void Update()
    {
        //Only check if it is visible while its being rendered
        if (isRendered)
        {
            CheckIfSeen(aimCamera);
        }
    }
    
    public void Hit()
    {
        playerScript.visibleTargets.Remove(gameObject);
        target.Hit();
        Debug.Log("I got SHOT!!!");

        if (transform.parent != null && transform.parent.CompareTag("HermitCrabPart"))
        {
            HermitCrab crab = transform.parent.gameObject.GetComponentInParent<HermitCrab>();
            crab.activeShootables.Remove(gameObject);
            crab.Hit();

            Debug.Log("Handle boss functions");
        }

        Destroy(gameObject);
    }


    private void OnBecameVisible()
    {
        isRendered = true;
    }

    private void OnBecameInvisible()
    {
        isRendered = false;
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
        if (seenHandled == false)
        {
            playerScript.visibleTargets.Add(gameObject);
        }
        seenHandled = true;
    }

    void IsNotSeen()
    {
        if (seenHandled == true)
        {
            playerScript.visibleTargets.Remove(gameObject);
        }
        isTarget(false);
        seenHandled = false;
    }

    //What to do when it becomes the target.
    public void isTarget(bool isIt)
    {
        //selectedObj.SetActive(isIt);
    }
}
