using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraRotationController : MonoBehaviour
{
    public float rotationSpeed;
    private float pivotInputX;
    private float pivotInputZ;

    private Vector3 startPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        

        pivotInputX -= Input.GetAxisRaw("Mouse X") * (rotationSpeed * Time.deltaTime);
        pivotInputZ -= Input.GetAxisRaw("Mouse Y") * (rotationSpeed * Time.deltaTime);

        //Limitar el valor de la roatcion para que siempre este los valores -90 y 90
        pivotInputX = Mathf.Clamp(pivotInputX, -0.3f, 0.3f);
        pivotInputZ = Mathf.Clamp(pivotInputZ, -0.1f, 0.1f);

        transform.position = startPosition + new Vector3(pivotInputX, 0, pivotInputZ);

    }
}
