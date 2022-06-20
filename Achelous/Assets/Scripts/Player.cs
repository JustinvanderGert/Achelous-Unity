using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject target;
    public PlayerMovement_FirstPerson moveScript;
    public List<GameObject> visibleTargets = new List<GameObject>();
    public Transform projectileSpawnpoint;
    public GameObject projectile;

    public float minTargetingRange;
    public float shootingCooldown;

    bool rechargeShot = false;

    GameObject currentTarget;

    

    private void Start()
    {
        moveScript = gameObject.GetComponent<PlayerMovement_FirstPerson>();
        if (target == null)
        {
            target = GameObject.FindGameObjectWithTag("Slider");
            target.SetActive(false);
        }
    }

    void Update()
    {
        //If targets are visible find the closest
        if (visibleTargets.Count > 0)
        {
            target.SetActive(true);
            currentTarget = GetClosestTarget();

            target.transform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, currentTarget.transform.position);

            /*if (currentTarget.GetComponent<Target2>().currentCharge == 0)
            {
                currentTarget.GetComponent<Target2>().chargeState = Target2.ChargeStates.Charging;
            }*/

            //If you press left click, shoot
            if (!rechargeShot && Input.GetMouseButtonDown(0))
            {
                Shoot();
            }
        }
        else
        {
            target.SetActive(false);
            currentTarget = null;
        }
    }

    GameObject GetClosestTarget()
    {
        GameObject closestObj = null;
        float closestDist = -1;

        for (int i = 0; i < visibleTargets.Count; i++)
        {
            GameObject current = visibleTargets[i];

            float dist = Vector2.Distance(transform.position, current.transform.position);

            if (dist < closestDist || closestDist == -1)
            {
                closestObj = current;
                closestDist = dist;
            }
        }

        if(closestObj != currentTarget)
        {
            target.GetComponent<Target2>().ResetState();
        }
        return closestObj;
    }

    void Shoot()
    {
        StartCoroutine(Cooldown());
        GameObject Shotprojectile = Instantiate(projectile, projectileSpawnpoint.position, projectileSpawnpoint.rotation);
        Shotprojectile.GetComponentInChildren<Projectile>().SetTarget(currentTarget);
    }

    IEnumerator Cooldown()
    {
        rechargeShot = true;
        yield return new WaitForSeconds(shootingCooldown);
        rechargeShot = false;
    }

    void OnParticleTrigger()
    {
        Debug.Log("Hit the Player with the wave");
    }
}