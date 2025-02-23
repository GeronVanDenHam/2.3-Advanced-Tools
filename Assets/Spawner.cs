using UnityEngine;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    public GameObject spherePrefab;
    public GameObject meshPrefab;
    public GameObject capsulePrefab;

    [System.NonSerialized]
    public int numberOfObjects = 10;
    public float spawnRadius = 15f;

    private System.Random random = new System.Random(42);
    public static List<CollisionCounter> spawnedCounters = new List<CollisionCounter>();

    public enum ObjectType { Sphere, Mesh, Capsule }
    public static ObjectType selectedObjectType;

    private int remainingObjects;

    void Start()
    {
        //remainingObjects = numberOfObjects;
        spawnedCounters.Clear();
    }

    void FixedUpdate()
    {
        if (numberOfObjects > remainingObjects)
        {
            SpawnObject();
            remainingObjects++;
        }
    }

    void SpawnObject()
    {
        float angle = (float)random.NextDouble() * Mathf.PI * 2;
        float distance = (float)random.NextDouble() * spawnRadius;
        float x = Mathf.Cos(angle) * distance;
        float z = Mathf.Sin(angle) * distance;

        Vector3 spawnPosition = new Vector3(transform.position.x + x, transform.position.y, transform.position.z + z);
        GameObject selectedPrefab = GetPrefabFromEnum(selectedObjectType);

        if (selectedPrefab)
        {
            GameObject spawnedObject = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);
            CollisionCounter counter = spawnedObject.GetComponent<CollisionCounter>();

            if (counter == null)
                counter = spawnedObject.AddComponent<CollisionCounter>();

            spawnedCounters.Add(counter);
        }
    }

    public void SetSeed(int seed) => random = new System.Random(seed);

    GameObject GetPrefabFromEnum(ObjectType type) => type switch
    {
        ObjectType.Sphere => spherePrefab,
        ObjectType.Mesh => meshPrefab,
        ObjectType.Capsule => capsulePrefab,
        _ => null
    };
}
