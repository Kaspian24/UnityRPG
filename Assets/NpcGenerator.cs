using UnityEngine;
using System.Collections.Generic;

public class NpcGenerator : MonoBehaviour
{
    public GameObject npcPrefab1;
    public GameObject npcPrefab2;
    public GameObject npcPrefab3;
    public Vector3 minSpawnPosition = new Vector3(-5f, 0f, -5f);
    public Vector3 maxSpawnPosition = new Vector3(5f, 2f, 5f);
    public int numberOfNPCsToSpawn = 20;

    void Start()
    {
        SpawnNPCs();
    }

    void SpawnNPCs()
    {
        int count1 = Mathf.CeilToInt(numberOfNPCsToSpawn * 0.1f);
        int count2 = Mathf.CeilToInt(numberOfNPCsToSpawn * 0.2f);
        int count3 = numberOfNPCsToSpawn - count1 - count2;

        SpawnUniqueNPCs(npcPrefab1, count1);
        SpawnUniqueNPCs(npcPrefab2, count2);
        SpawnUniqueNPCs(npcPrefab3, count3);
    }

    void SpawnUniqueNPCs(GameObject npcPrefab, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPosition;
            float randomX = Random.Range(minSpawnPosition.x, maxSpawnPosition.x);
            float randomY = Random.Range(minSpawnPosition.y, maxSpawnPosition.y);
            float randomZ = Random.Range(minSpawnPosition.z, maxSpawnPosition.z);
            spawnPosition = new Vector3(randomX, randomY, randomZ);
            Instantiate(npcPrefab, spawnPosition, Quaternion.identity);
        }
    }
}