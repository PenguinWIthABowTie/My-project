using UnityEngine;
using UnityEngine.Rendering;

public class FirstPersonPlayer : MonoBehaviour
{
    public float movementSpeed = 4;
    public float rotationSpeed = 10;
    public float jumpForce = 6;

    public Transform pivot;

    //Guardar el rotacion en el eje X que necesita el pivote para mirrar arriba y abajo
    private float pivotRotationX;
    private CharacterController cc;
    private Vector3 gravity;

    public bool isGrounded;

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        //MOVEMIENTO//

        isGrounded = cc.isGrounded;
        //Guardar el valor de movimiento en los ejes X Z
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        //Hay que cambiar el vector de input para que sea relativo al transform del jugarod y siempre
        //se mueva segun hacia donde esta mirando y no relativo al mundo 
        input = transform.TransformDirection(input);

        if (Input.GetButtonDown("Jump") && cc.isGrounded == true)
        {
            //Codigo magico para saltar con el CharacterController
            gravity.y = Mathf.Sqrt(jumpForce * -2.0f * 9.8f);
        }

        //Aplicar Gravedad manual
        gravity.y -= 9.8f * Time.deltaTime;

        //Mientras toca el suelo, no se le aplica gravedad
        if (cc.isGrounded == true)
        {
            gravity.y = 0;
        }

        //Mover al personaje usando el propio CharacterController
        cc.Move(input * movementSpeed * Time.deltaTime + gravity * Time.deltaTime);

        //ROTACION//

        //Rotacion horizontal
        transform.Rotate(new Vector3(0, Input.GetAxisRaw("Mouse X") * rotationSpeed, 0));

        //Al valor actual de rotacion le sumamos o restamos segun si subimos o bajamos el rato
        pivotRotationX -= Input.GetAxisRaw("Mouse Y") * rotationSpeed;

        //Limitar el valor de la roatcion para que siempre este los valores -90 y 90
        pivotRotationX = Mathf.Clamp(pivotRotationX, -90, 90);

        //Actualizar los angulos de rotacion del pivote para que su rotacion en X sea la que hemos calculado con el raton
        pivot.localEulerAngles = new Vector3(pivotRotationX, 0, 0);

        if (Input.GetKeyDown(KeyCode.F1))
        {
            transform.position = new Vector3(0, 1, 0);
        }

    }
}
