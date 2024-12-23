using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab; // Assign your enemy prefab in the Inspector
    [SerializeField] private int minEnemies = 10;
    [SerializeField] private int maxEnemies = 15;

    private void Start()
    {
        SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        int enemyCount = Random.Range(minEnemies, maxEnemies + 1); // Random count between 10 and 15

        for (int i = 0; i < enemyCount; i++)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        // Define the boundaries for spawning
        float spawnX = Random.Range(-31f, 31f); // Adjust these values based on your map
        float spawnY = Random.Range(-3.5f, 41f);
        Vector2 spawnPosition = new Vector2(spawnX, spawnY);

        // Instantiate the enemy prefab at the spawn position
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}