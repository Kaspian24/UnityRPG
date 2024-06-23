using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // Przeciwnik prefab
    public float spawnDistance = 100.0f; // Odleg³oœæ, w której przeciwnik siê spawnuje
    float timer;

    private Transform playerTransform; // Referencja do gracza

    void Start()
    {
        // ZnajdŸ gracza po tagu
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

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

    void SpawnEnemies()
    {
        Vector3 spawnPosition = transform.position + Random.insideUnitSphere * 10;
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        timer = 0;
    }
}