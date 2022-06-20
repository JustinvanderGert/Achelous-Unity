using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Shrine : MonoBehaviour
{
    SoundManager soundManager;

    public float activationRange = 5f;
    public TMP_Text activationText;

    bool activated = false;
    GameObject player;
    public AudioClip ShrineMusic;
    AudioSource audioSource;


    private void Start()
    {
        soundManager = GameObject.FindObjectOfType<SoundManager>();
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
        soundManager.PlayShrine(ShrineMusic);
        //audioSource.Play();
        activationText.gameObject.SetActive(false);
        activated = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, activationRange);
    }
}
