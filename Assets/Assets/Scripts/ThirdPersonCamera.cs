using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private float distance = 5f;
    [SerializeField] private float mouseSensitivity = 3f;

    [SerializeField] private float minVerticalAngle = -20f;
    [SerializeField] private float maxVerticalAngle = 70f;

    // Collision detection parameters
    [SerializeField] private float collisionRaduis = 0.3f;
    // The collision offset is used to prevent the camera from getting too close to the target when a collision is detected. It ensures that the camera maintains a small distance from the target, avoiding clipping issues.
    [SerializeField] private float collisionOffset = 0.2f;
    [SerializeField] private LayerMask collisionLayers;

    private float yaw;
    private float pitch;
    private Quaternion rotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
        if(Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseY = Input.GetAxis("Mouse Y");
            // Update yaw and pitch based on mouse input
            yaw += mouseX * mouseSensitivity;
            pitch += mouseY * mouseSensitivity;
            // Clamp the pitch to prevent the camera from flipping over
            pitch = Mathf.Clamp(pitch, minVerticalAngle, maxVerticalAngle);
            // Calculate the new camera position based on yaw, pitch, and distance
            rotation = Quaternion.Euler(pitch, yaw, 0f);
        }
        // Calculate the direction from the target to the camera
        Vector3 direction = rotation * Vector3.back;
        // Calculate the desired camera position based on the target's position and the direction
        float currentDistance = distance;

        // Check for collisions using a sphere cast
        if (Physics.SphereCast(target.position, collisionRaduis, direction, out RaycastHit hit, distance, collisionLayers))
        {
            // If a collision is detected, adjust the camera's distance to avoid clipping into objects
            currentDistance = hit.distance - collisionOffset;
        }

        // Set the camera's position and rotation
        transform.position = target.position + direction * currentDistance;
        // Make the camera look at the target
        transform.LookAt(target);
    }
}
