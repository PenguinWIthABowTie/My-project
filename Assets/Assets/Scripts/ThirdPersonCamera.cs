using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private float distance = 5f;
    [SerializeField] private float mouseSensitivity = 3f;

    [SerializeField] private float minVerticalAngle = -20f;
    [SerializeField] private float maxVerticalAngle = 70f;
    [SerializeField] private Quaternion rotation;

    private float yaw;
    private float pitch;

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
        if(Input.GetMouseButtonUp(1))
        {
        }
        // Set the camera's position and rotation
        transform.position = target.position + rotation * new Vector3(0f, 0f, -distance);
        // Make the camera look at the target
        transform.LookAt(target);
    }
}
