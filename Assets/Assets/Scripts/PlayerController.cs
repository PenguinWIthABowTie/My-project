using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float movementSpeed = 5f;
    public float rotationSpeed = 10f;

    [Header("Camera")]
    public Transform cameraTransform;
    public Transform pivot;
    public float cameraRotationSpeed = 3f;

    private Rigidbody rb;
    private Vector3 input;
    private Vector3 movementDirection;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void Update()
    {
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

        if (Input.GetMouseButton(1))
        {
            pivot.Rotate(0f, Input.GetAxis("Mouse X") * cameraRotationSpeed, 0f);
        }


        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;

        cameraForward.Normalize();
        cameraRight.Normalize();


        movementDirection = cameraForward * input.z + cameraRight * input.x;

        movementDirection = Vector3.ClampMagnitude(movementDirection, 1f);

    }

    private void FixedUpdate()
    {
        Vector3 velocity = movementDirection * movementSpeed;

        velocity.y = rb.linearVelocity.y;

        rb.linearVelocity = velocity;


        if (movementDirection.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);

            Quaternion smoothRotation = Quaternion.Slerp( rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);

            rb.MoveRotation(smoothRotation);
        }
    }



}
