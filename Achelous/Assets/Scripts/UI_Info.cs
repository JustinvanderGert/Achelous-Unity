using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UI_Info : MonoBehaviour
{
    GameObject player;

    [SerializeField]
    GameObject interactPrompt;

    bool scanning = false;


    public float activeDistance = 10;
    public GameObject uiElement;



    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        interactPrompt = GameObject.FindGameObjectWithTag("interactPrompt");
        uiElement.SetActive(false);
    }


    void Update()
    {
        if(uiElement.activeSelf)
            uiElement.transform.LookAt(2 * transform.position - player.transform.position);

        if (!scanning)
        {
            float distance = Vector3.Distance(uiElement.transform.position, player.transform.position);

            if (distance <= activeDistance)
                interactPrompt.SetActive(true);
            else
                interactPrompt.SetActive(false);
        }
        else
        {
            float distance = Vector3.Distance(uiElement.transform.position, player.transform.position);
            if (distance > activeDistance + 5)
            {
                uiElement.SetActive(false);
                scanning = false;
            }
        }

        if(interactPrompt.activeSelf && Input.GetKeyDown(KeyCode.E))
        {
            scanning = true;
            interactPrompt.SetActive(false);
            uiElement.SetActive(true);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, activeDistance);
    }
}