using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public List<GameObject> fish = new List<GameObject>();

    public float spawnAmount = 50;
    public Vector3 spawnSize = new Vector3(10, 10, 10);

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
            float currentWeight = fish[i].GetComponent<FishSpawnSettings>().spawnWeight;

            float timesToSpawn = totalWeight / fish.Count / spawnAmount * currentWeight * 100;
            Debug.Log("Times: " + timesToSpawn);

            for (int b = 0; b < timesToSpawn; b++)
            {
                Vector3 spawnPosition = transform.position + new Vector3(Random.Range(0, spawnSize.x), Random.Range(0, spawnSize.y), Random.Range(0, spawnSize.z)) - spawnSize / 2;
                Debug.Log(spawnPosition);
                Instantiate(fish[i], spawnPosition, transform.rotation, gameObject.transform);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, spawnSize);
    }
}