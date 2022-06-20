using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public List<GameObject> fish = new List<GameObject>();

    public float spawnAmount = 50;
    public Vector3 spawnSize = new Vector3(10, 10, 10);
    int layerMask = 1 << 3;

    List<float> fishWeights = new List<float>();
    float totalWeight;


    void Start()
    {
        for (int i = 0; i < fish.Count; i++)
        {
            fishWeights.Add(fish[i].GetComponent<FishSpawnSettings>().spawnWeight);
            totalWeight += fish[i].GetComponent<FishSpawnSettings>().spawnWeight;
        }

        for (int i = 0; i < fish.Count; i++)
        {
            int retries = 0;
            float currentWeight = fish[i].GetComponent<FishSpawnSettings>().spawnWeight;
            float timesToSpawn = 100 / (100 / spawnAmount * totalWeight) * currentWeight;

            for (int b = 0; b < timesToSpawn; b++)
            {
                Vector3 spawnPosition = transform.position + new Vector3(Random.Range(0, spawnSize.x), Random.Range(0, spawnSize.y), Random.Range(0, spawnSize.z)) - spawnSize / 2;
                GameObject fishObject = Instantiate(fish[i], spawnPosition, transform.rotation, gameObject.transform);
                if(Physics.Raycast(fishObject.transform.position, -Vector3.up, spawnSize.y, layerMask) && Physics.OverlapSphere(fishObject.transform.position, 0.1f).Length <= 1)
                {
                    retries = 0;
                }
                else
                {
                    Destroy(fishObject);
                    b--;

                    if (retries >= 10)
                    {
                        Debug.Log("Max attempts reached");
                        b++;
                    }
                }

            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, spawnSize);
    }
}