using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public Vector3 spawnArea;
    public int numberOfSpheres = 20;

    public Vector3 minPosition = new Vector3(-5f, 0f, -5f); 
    public Vector3 maxPosition = new Vector3(5f, 2f, 5f); 

    void Start()
    {
        SpawnSpheres();
    }

    void SpawnSpheres()
    {
        for (int i = 0; i < numberOfSpheres; i++)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(minPosition.x, maxPosition.x), Random.Range(minPosition.y, maxPosition.y), Random.Range(minPosition.z, maxPosition.z));
            spawnPosition += transform.position;
            
            GameObject newSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            newSphere.transform.position = spawnPosition;
        }
    }
}
