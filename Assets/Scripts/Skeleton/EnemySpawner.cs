using UnityEngine;

/// <summary>
/// Spawns skeletons around the player within a specified distance.
/// </summary>
public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnDistance = 100.0f;
    float timer;
    private Transform playerTransform;

    /// <summary>
    /// Initializes the playerTransform reference when the script starts.
    /// </summary>
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    /// <summary>
    /// Checks the distance to the player and spawns enemies if within spawnDistance.
    /// </summary>
    void Update()
    {
        if (playerTransform != null)
        {
            float distance = Vector3.Distance(playerTransform.position, transform.position);
            if (distance <= spawnDistance)
            {
                timer += Time.deltaTime;
                if(timer > 10f)
                    SpawnEnemies();
            }
        }
    }

    /// <summary>
    /// Spawns an enemy at a random position around the spawner within a radius of 20 units.
    /// </summary>
    void SpawnEnemies()
    {
        Vector3 randomPosition2D = Random.insideUnitCircle * 20;
        Vector3 spawnPosition = new Vector3(transform.position.x + randomPosition2D.x, transform.position.y, transform.position.z + randomPosition2D.y);
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        timer = 0;
    }
}