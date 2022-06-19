using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Shrine : MonoBehaviour
{
    public float activationRange = 5f;
    public Text activationText;

    bool activated = false;
    GameObject player;
    AudioSource audioSource;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if(distance < activationRange && !activated)
            activationText.gameObject.SetActive(true);

        else
            activationText.gameObject.SetActive(false);
        

        if (activationText.gameObject.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.E))
                ActivateShrine();
        }
    }

    void ActivateShrine()
    {
        Debug.Log("Activated shrine");
        audioSource.Play();
        activationText.gameObject.SetActive(false);
        activated = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, activationRange);
    }
}
