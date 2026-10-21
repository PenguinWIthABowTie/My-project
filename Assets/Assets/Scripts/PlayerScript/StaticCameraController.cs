using UnityEngine;



public class StaticCameraController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 5f;

    private Vector3 input;

    private Rigidbody rb;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        input = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        //Hacer que el movemiento sea relativo a la camara y a lo que esta viendo
        input = Camera.main.transform.TransformDirection(input);
        //LookRotation calcula la rotation necesaria para mirar hacia una direccion concreta
        //En este cas, queremos que el personaje rote para mirar en la direction en la que se mueve (input)
        if (input.sqrMagnitude > 0)
        { 
            Vector3 lookRotation = Quaternion.LookRotation(input).eulerAngles;

            lookRotation.x = 0;
            transform.rotation = Quaternion.Slerp(Quaternion.LookRotation(input), Quaternion.LookRotation(input), rotationSpeed);

        }
    }

    private void FixedUpdate()
    {

        Vector3 movement = input.normalized * moveSpeed;
        //mantener la velocidad y original para no reventar la gravedad
        movement.y = rb.linearVelocity.y;
        rb.linearVelocity = movement;
    }
}
