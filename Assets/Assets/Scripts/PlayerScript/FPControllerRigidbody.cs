using UnityEngine;

public class FPControllerRigidbody : MonoBehaviour
{
    public float movementSpeed = 4;
    public float rotationSpeed = 10;
    public float jumpForce = 6;

    public Transform pivot;

    //Guardar el rotacion en el eje X que necesita el pivote para mirrar arriba y abajo
    private float pivotRotationX;

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

            //Rotacion horizontal
        transform.Rotate(new Vector3(0, Input.GetAxisRaw("Mouse X") * rotationSpeed, 0));

        //Al valor actual de rotacion le sumamos o restamos segun si subimos o bajamos el rato
        pivotRotationX -= Input.GetAxisRaw("Mouse Y") * rotationSpeed;

        //Limitar el valor de la roatcion para que siempre este los valores -90 y 90
        pivotRotationX = Mathf.Clamp(pivotRotationX, -90, 90);

        //Actualizar los angulos de rotacion del pivote para que su rotacion en X sea la que hemos calculado con el raton
        pivot.localEulerAngles = new Vector3(pivotRotationX, 0, 0);
    }

    private void FixedUpdate()
    {

        Vector3 movement = input.normalized * movementSpeed;
        //mantener la velocidad y original para no reventar la gravedad
        movement.y = rb.linearVelocity.y;
        rb.linearVelocity = movement;
    }
}