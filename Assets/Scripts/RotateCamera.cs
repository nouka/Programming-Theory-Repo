using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    readonly float rotationSpeed = 200;

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, horizontalInput * rotationSpeed * Time.deltaTime);
    }
}
