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
        for(noOfBoxes = 0; noOfBoxes < 20; noOfBoxes++)
        {
            Vector3 randomSpawnPosition = new Vector3(Random.Range(-10, 11), Random.Range(1, 11), Random.Range(-10, 11));
            Instantiate(CubePrefab, randomSpawnPosition, Quaternion.identity);

        }

    }
}
