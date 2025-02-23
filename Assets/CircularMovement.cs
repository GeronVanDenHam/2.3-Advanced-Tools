using UnityEngine;

public class CircularMovement : MonoBehaviour
{

    public float rotationSpeed = 50f; // Speed of rotation in degrees per second

    void Update()
    {
        // Rotate the cube around its own Y-axis (change axes as needed)
        transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
    }
}