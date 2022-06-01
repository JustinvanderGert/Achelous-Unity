using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameObject player;


    public List<GameObject> AllTargets = new List<GameObject>();
    public GameObject targets;



    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    void Update()
    {
        if(Input.GetButtonDown("r"))
        {
            SceneManager.LoadScene(0);
        }
    }

    void SpawnTargets()
    {
        //Spawn new targets here
    }

    void DeleteTargets()
    {
        //Delete old targets here (Do we want to reuse them instead?).
        for(int i = 0; i < AllTargets.Count; i++)
        {
            GameObject current = AllTargets[i];
            Destroy(current);
        }
    }
}