using UnityEngine;
using System.IO;
using System;

public class DataCollector : MonoBehaviour
{
    public float recordingDuration = 10f; // Set from config file via GameManager
    public float logInterval = 0.1f; // Log data every 100ms

    private float elapsedTime = 0f;
    private int frameCount = 0;
    private string filePath;
    private StreamWriter writer;

    float deltaTime = 0.0f;
    int fps = 0; // FPS stored as an integer

    void Start()
    {
        string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        filePath = Path.Combine(Application.dataPath, $"CollisionData_{timestamp}.csv");

        writer = new StreamWriter(filePath);
        writer.WriteLine("Frame,Time (s),FPS,Collisions,Active Objects,Collider Type");

        InvokeRepeating(nameof(LogData), 0f, logInterval);
    }

    void Update()
    {
        deltaTime += Time.deltaTime;
        deltaTime /= 2.0f;
        fps = Mathf.RoundToInt(1.0f / deltaTime); // Round FPS to integer
    }

    void LogData()
    {
        if (elapsedTime >= recordingDuration)
        {
            StopRecording();
            return;
        }

        frameCount++;
        elapsedTime += logInterval;

        int collisionCount = CollisionCounter.GetTotalCollisions();
        int activeObjects = Spawner.spawnedCounters.Count;
        string colliderType = Spawner.selectedObjectType.ToString();

        writer.WriteLine($"{frameCount},{elapsedTime:F0},{fps},{collisionCount},{activeObjects},{colliderType}");
    }

    void StopRecording()
    {
        CancelInvoke(nameof(LogData));
        writer.Close();
        Debug.Log($"Data logging finished. File saved at: {filePath}");
        Application.Quit();
    }

    void OnDestroy()
    {
        if (writer != null)
        {
            writer.Close();
        }
    }
}
