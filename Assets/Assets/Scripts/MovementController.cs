using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.XR;

public class MovementController : MonoBehaviour
{
    //public float moveSpeed = 5f;
    //private Vector2 input;
    //public float targetRotation = 0.0f;
    //public float rotationVelocity;
    //public float verticalVelocity;
    //public float RotationSmoothTime = 0.12f;
    private Rigidbody rb;
    private GroundChecker groundChecker;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 20f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private Transform cameraTransform;
    private Vector3 moveDirection;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        groundChecker = GetComponent<GroundChecker>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;

        cameraForward.y = 0f;
        cameraRight.y = 0f;

        cameraForward.Normalize();
        cameraRight.Normalize();

        moveDirection = cameraForward * vertical + cameraRight * horizontal;
        moveDirection = Vector3.ClampMagnitude(moveDirection, 1f);

        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        if (Input.GetButtonDown("Jump") && groundChecker.isGrounded == true)
        {
            Debug.Log("*");
            rb.AddForce(Vector3.up * (jumpForce * 10), ForceMode.Force);

        }

        //input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        //Vector3 inputDirection = new Vector3(input.x, 0.0f, input.y).normalized;

        //if (input != Vector2.zero)
        //{
        //    Debug.Log("*");
        //    targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg +
        //                      mainCamera.transform.eulerAngles.y;
        //    float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref rotationVelocity,
        //        RotationSmoothTime);

        //    // rotate to face input direction relative to camera position
        //    transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        //}
        //else
        //{
        //    moveSpeed = 0.0f;
        //}
        //    Vector3 targetDirection = Quaternion.Euler(0.0f, targetRotation, 0.0f) * Vector3.forward;

        //// move the player
        //controller.Move(targetDirection.normalized * (moveSpeed * Time.deltaTime) +
        //                 new Vector3(0.0f, verticalVelocity, 0.0f) * Time.deltaTime);
    }
    private void FixedUpdate()
    {
        Vector3 velocity = rb.linearVelocity;

        velocity.x = moveDirection.x * moveSpeed;
        velocity.z = moveDirection.z * moveSpeed;

        rb.linearVelocity = velocity;
    }

}
