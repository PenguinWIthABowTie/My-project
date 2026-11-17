using UnityEngine;

public class InteractableDetector : MonoBehaviour
{
    //Cuanto alcance tiene rayo
    [SerializeField] private float detectionRange = 1;
    //Radio de la esfera que se lanza a traves del rayo
    [SerializeField] private float sphereRadius = 1;
    //La capa en la que estran los objetos que pueden recogerse
    [SerializeField] private LayerMask interactLayers;
    //Referencia al objeto que este detectando en eso momento
    [SerializeField] private ItemPickup detectedInteractable;


    private void Update()
    {
        Detect();
    }

    void Detect()
    {
        //Crea cintinuamente una esfera a su alredador para detectae objetos cercamos
        Collider[] interactObject = Physics.OverlapSphere(transform.position, detectionRange, interactLayers);

        Debug.Log(interactObject);

    }



    private void OnDrawGizmos()
    {
        //Representation visual del SphereCast (aproximada)
        Gizmos.color = Color.cornflowerBlue;
        Gizmos.DrawWireSphere(transform.position, sphereRadius);
    }



}
