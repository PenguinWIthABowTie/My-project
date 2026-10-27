using UnityEngine;

public class FPControllerRigidbody : MonoBehaviour
{
    public float movementSpeed = 4;
    public float rotationSpeed = 10;
    public float jumpForce = 6;

    public Transform pivot;

    //Guardar el rotacion en el eje X que necesita el pivote para mirrar arriba y abajo
    private float pivotRotationX;
    private float pivotRotationY;

    //Almacentar el input del jugador
    private Vector3 input;

    private Rigidbody rb;
    private GroundChecker groundChecker;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        groundChecker = GetComponent<GroundChecker>();
    }

    void Update()
    {
                            //MOVEMIENTO//


        //Guardar el valor de movimiento en los ejes X Z
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        //Hay que cambiar el vector de input para que sea relativo al transform del jugarod y siempre
        //se mueva segun hacia donde esta mirando y no relativo al mundo 
        input = transform.TransformDirection(input);

        if (Input.GetButtonDown("Jump") && groundChecker.isGrounded == true)
        {
            Debug.Log("*");
            rb.AddForce(Vector3.up * (jumpForce * 10), ForceMode.Force);

        }


        //ROTACION//
        //Al valor actual de rotacion le sumamos o restamos segun si subimos o bajamos el rato
        //pivotRotationX -= Input.GetAxisRaw("Mouse Y") * rotationSpeed;
        if(!Input.GetMouseButton(1))
        {
            Debug.Log("Boton derecho del raton no pulsado");
            pivot.Rotate(new Vector3(0, Input.GetAxisRaw("Mouse X") * rotationSpeed, 0));
        }

        if (input.sqrMagnitude > 0.01f)
        {
            Vector3 lookDirection = new Vector3(input.x, 0f, input.z);

            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        }


        ////Limitar el valor de la roatcion para que siempre este los valores -90 y 90
        // pivotRotationY = Mathf.Clamp(pivotRotationY, -90, 90);

        ////Actualizar los angulos de rotacion del pivote para que su rotacion en X sea la que hemos calculado con el raton
        // pivot.localEulerAngles = new Vector3(0, pivotRotationX, 0);

        //if (Input.GetMouseButton(1))
        //{
        //    Debug.Log("Boton derecho del raton pulsado");
        //    //Rotacion horizontal
        //    transform.Rotate(new Vector3(0, Input.GetAxisRaw("Mouse X") * rotationSpeed, 0));
        //}
    }

    private void FixedUpdate()
    {

        Vector3 movement = input.normalized * movementSpeed;
        //mantener la velocidad y original para no reventar la gravedad
        movement.y = rb.linearVelocity.y;
        rb.linearVelocity = movement;
    }
}