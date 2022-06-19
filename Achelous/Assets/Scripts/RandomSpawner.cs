using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    [SerializeField]
    int noOfTrash = 5;
    int layerMask = 1 << 3;
    int index;

    public Vector3 spawnSize;

    public List<GameObject> allPlastic = new List<GameObject>();
    public List<GameObject> plastics = new List<GameObject>();



    void Start()
    {
        if (spawnSize == new Vector3(0, 0, 0))
            spawnSize = new Vector3(10, 10, 10);

        SpawnPlastic(noOfTrash);
    }

    void SpawnPlastic(int amount)
    {
        int retries = 0;
        for (int i = 0; i < amount; i++)
        {
            int plasticToSpawn = Random.Range(0, plastics.Count - 1);

            Vector3 randomSpawnPosition = transform.position + new Vector3(Random.Range(0, spawnSize.x), Random.Range(0, spawnSize.y), Random.Range(0, spawnSize.z)) - spawnSize / 2;

            GameObject plastic = Instantiate(plastics[plasticToSpawn], randomSpawnPosition, Quaternion.identity);

            if (Physics.Raycast(plastic.transform.position, -Vector3.up, spawnSize.y, layerMask) && Physics.OverlapSphere(plastic.transform.position, 0.1f).Length <= 1)
            {
                plastic.GetComponent<Shootables>().spawner = gameObject;
                allPlastic.Add(plastic);
            }
            else
            {
                Destroy(plastic);
                i--;
                retries++;
            }
            if(retries >= 10)
            {
                i++;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, spawnSize);
    }


    public void PlasticHit()
    {
        int amountToSpawn = noOfTrash - allPlastic.Count;
        SpawnPlastic(amountToSpawn);
    }
}