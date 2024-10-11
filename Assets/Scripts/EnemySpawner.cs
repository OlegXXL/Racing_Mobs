using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform car;
    [SerializeField] private Transform finish;
    [SerializeField] private int minEnemies = 1; 
    [SerializeField] private int maxEnemies = 5; 
    [SerializeField] private float minDistanceFromCar = 10f; 
    [SerializeField] private float maxDistanceFromFinish = 20f; 

    void Start()
    {
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        float totalDistance = Vector3.Distance(car.position, finish.position);

        int enemyCount = Random.Range(minEnemies, maxEnemies + 1);

        for (int i = 0; i < enemyCount; i++)
        {
            float randomDistance = Random.Range(minDistanceFromCar, totalDistance - maxDistanceFromFinish);

            Vector3 direction = (finish.position - car.position).normalized;
            Vector3 spawnPosition = car.position + direction * randomDistance;

            spawnPosition += new Vector3(Random.Range(-10f, 10f), 0, Random.Range(-10f, 10f));

            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }
}