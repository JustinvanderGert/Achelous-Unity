using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    [SerializeField]
    int noOfBoxes = 5;


    public Vector3 spawnSize;

    public List<GameObject> allPlastic = new List<GameObject>();
    public List<GameObject> plastics = new List<GameObject>();



    void Start()
    {
        for(int i = 0; i < noOfBoxes; i++)
        {
            SpawnPlastic();
        }
    }
    
    void SpawnPlastic()
    {
        int plasticToSpawn = Random.Range(0, plastics.Count - 1);
        //Vector3 randomSpawnPosition = new Vector3(Random.Range(-20, 21), Random.Range(1, 11), Random.Range(-20, 21));
        Vector3 randomSpawnPosition = transform.position + new Vector3(Random.Range(0, spawnSize.x), Random.Range(0, spawnSize.y), Random.Range(0, spawnSize.z)) - spawnSize / 2;
        GameObject plastic = Instantiate(plastics[plasticToSpawn], randomSpawnPosition, Quaternion.identity);

        plastic.GetComponent<Shootables>().spawner = gameObject;
        allPlastic.Add(plastic);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, spawnSize);
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