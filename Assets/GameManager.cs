using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
    public Spawner spawner;
    public CircularMovement cubeRotation;
    public DataCollector dataCollector;

    private string configPath;

    void Start()
    {
        configPath = Path.Combine(Application.dataPath, "config.txt");
        LoadConfig();
    }

    void LoadConfig()
    {
        if (!File.Exists(configPath))
        {
            Debug.LogError("Config file not found: " + configPath);
            return;
        }

        string[] lines = File.ReadAllLines(configPath);
        foreach (string line in lines)
        {
            string[] parts = line.Split('=');
            if (parts.Length != 2) continue;

            string key = parts[0].Trim();
            string value = parts[1].Trim();

            switch (key)
            {
                case "rotationSpeed":
                    if (cubeRotation) cubeRotation.rotationSpeed = float.Parse(value);
                    break;
                case "selectedObjectType":
                    if (spawner && System.Enum.TryParse(value, out Spawner.ObjectType objType))
                        Spawner.selectedObjectType = objType;
                    break;
                case "randomSeed":
                    if (spawner) spawner.SetSeed(int.Parse(value));
                    break;
                case "spawnRadius":
                    if (spawner) spawner.spawnRadius = float.Parse(value);
                    break;
                case "numberOfObjects":
                    if (spawner) spawner.numberOfObjects = int.Parse(value);
                    break;
                case "recordingDuration":
                    if (dataCollector) dataCollector.recordingDuration = float.Parse(value);
                    break;
            }
        }
    }
}
