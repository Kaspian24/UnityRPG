using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnDistance = 100.0f;
    float timer;

    private Transform playerTransform;
    void Start()
    {
        // Znajdü gracza po tagu
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
        Vector3 randomPosition2D = Random.insideUnitCircle * 20;
        Vector3 spawnPosition = new Vector3(transform.position.x + randomPosition2D.x, transform.position.y, transform.position.z + randomPosition2D.y);
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        timer = 0;
    }
}