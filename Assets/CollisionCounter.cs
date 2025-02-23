using UnityEngine;
using System.Collections.Generic;

public class CollisionCounter : MonoBehaviour
{
    private int collisionCount = 0;

    void OnCollisionEnter(Collision collision)
    {
        collisionCount++;
    }

    public int GetCollisionCount() => collisionCount;

    public static int GetTotalCollisions()
    {
        int total = 0;
        foreach (CollisionCounter counter in Spawner.spawnedCounters)
        {
            total += counter.GetCollisionCount();
        }
        return total;
    }
}
