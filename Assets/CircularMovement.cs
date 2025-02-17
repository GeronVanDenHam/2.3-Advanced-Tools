using UnityEngine;

public class CircularMovement : MonoBehaviour
{
    public float radius = 5f;  // Radius of the circular path
    public float speed = 2f;   // Speed of movement

    private float angle = 0f;

    void Update()
    {
        // Increment angle over time based on speed
        angle += speed * Time.deltaTime;

        // Calculate new X and Z positions using Sin and Cos
        float x = Mathf.Cos(angle) * radius;
        float z = Mathf.Sin(angle) * radius;

        // Apply new position while keeping Y position the same
        transform.position = new Vector3(x, transform.position.y, z);
    }
}