using UnityEngine;

public class NpcGenerator : MonoBehaviour
{
    public GameObject npcPrefab;
    public Vector3 minSpawnPosition = new Vector3(-5f, 0f, -5f);
    public Vector3 maxSpawnPosition = new Vector3(5f, 2f, 5f);
    public int numberOfNPCsToSpawn = 20;

    void Start()
    {
        SpawnNPCs();
    }

    void SpawnNPCs()
    {
        for (int i = 0; i < numberOfNPCsToSpawn; i++)
        {
            float randomX = Random.Range(minSpawnPosition.x, maxSpawnPosition.x);
            float randomY = Random.Range(minSpawnPosition.y, maxSpawnPosition.y);
            float randomZ = Random.Range(minSpawnPosition.z, maxSpawnPosition.z);
            Vector3 spawnPosition = new Vector3(randomX, randomY, randomZ);

            GameObject newNPC = Instantiate(npcPrefab, spawnPosition, Quaternion.identity);
        }
    }
}
