using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    [SerializeField]
    int noOfBoxes = 5;


    public GameObject CubePrefab;

    public List<GameObject> allPlastic = new List<GameObject>();



    void Start()
    {
        for(int i = 0; i < noOfBoxes; i++)
        {
            SpawnPlastic();
        }
    }
    
    void SpawnPlastic()
    {
        Vector3 randomSpawnPosition = new Vector3(Random.Range(-20, 21), Random.Range(1, 11), Random.Range(-20, 21));
        GameObject plastic = Instantiate(CubePrefab, randomSpawnPosition, Quaternion.identity);

        plastic.GetComponent<Shootables>().spawner = gameObject;
        allPlastic.Add(plastic);
    }


    public void PlasticHit()
    {
        int amountToSpawn = noOfBoxes - allPlastic.Count;

        for (int i = 0; i < amountToSpawn; i++)
        {
            SpawnPlastic();
        }
    }
}