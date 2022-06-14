using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject CubePrefab;
    [SerializeField]
       int noOfBoxes = 0;

    void Start()
    {
        for(int i = 0; i < noOfBoxes; i++)
        {
            Vector3 randomSpawnPosition = new Vector3(Random.Range(-20, 21), Random.Range(1, 11), Random.Range(-20, 21));
            Instantiate(CubePrefab, randomSpawnPosition, Quaternion.identity);

        }

    }
}
