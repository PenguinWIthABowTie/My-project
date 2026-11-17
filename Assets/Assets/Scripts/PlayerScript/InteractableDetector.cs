using UnityEngine;
using UnityEngine.Events;

public class InteractableDetector : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    //Cuanto alcance tiene rayo
    [SerializeField] private float interactionRange;
    [SerializeField] private float viewAngle = 45f;
    ////Radio de la esfera que se lanza a traves del rayo
    //[SerializeField] private float sphereRadius = 1;
    //La capa en la que estran los objetos que pueden recogerse
    [SerializeField] private LayerMask interactLayers;
    //Referencia al objeto que este detectando en eso momento
    //[SerializeField] private ItemPickup detectedInteractable;

    public Interactable currentInteractable;

    void Update()
    {
        //Detect();
        CheckForInteractable();

        if(Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E");
            if (currentInteractable != null)
            {
                currentInteractable.InteractWith();
            }
        }
    }

    void CheckForInteractable()
    {
        currentInteractable = null;

        Collider[] colliders = Physics.OverlapSphere(transform.position, interactionRange, interactLayers);

        float closestAngle = viewAngle;

        foreach (Collider collider in colliders)
        {
            Interactable interactable = collider.GetComponentInParent<Interactable>();

            if (interactable == null) continue;

            Vector3 direction = (collider.transform.position - playerCamera.transform.position).normalized;

            float angle = Vector3.Angle(playerCamera.transform.forward, direction);

            if (angle < closestAngle)
            {
                closestAngle = angle;
                currentInteractable = interactable;
            }
        }


        //if (Physics.Raycast(ray, out RaycastHit hit, interactionRange * 3f, interactLayers))
        //{
        //    float distance = Vector3.Distance(transform.position, hit.point);

        //    if(distance <= interactionRange)
        //    {
        //        currentInteractable = hit.collider.GetComponentInParent<Interactable>();
        //        Debug.Log("Detected interactable: " + hit.collider.name);
        //    }
        //}
    }

    //void Interact()
    //{
    //    if(currentInteractable != null)
    //    {
    //        currentInteractable.InteractWith();
    //    }
    //}



    //void Detect()
    //{
    //    //Crea cintinuamente una esfera a su alredador para detectae objetos cercamos
    //    Collider[] interactObject = Physics.OverlapSphere(transform.position, interactionRange, interactLayers);

    //    Debug.Log(interactObject);

    //}



    private void OnDrawGizmos()
    {
        //Representation visual del SphereCast (aproximada)
        Gizmos.color = Color.cornflowerBlue;
        Gizmos.DrawWireSphere(transform.position, interactionRange);

        //Gizmos.color = Color.cornflowerBlue;
        //Gizmos.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * (interactionRange * 3f));

        Vector3 forward = playerCamera.transform.forward;
        Vector3 leftBoundary = Quaternion.Euler(0, -viewAngle, 0) * forward;
        Vector3 rightBoundary = Quaternion.Euler(0, viewAngle, 0) * forward;

        Gizmos.color = Color.yellow;

        Gizmos.DrawRay(playerCamera.transform.position, leftBoundary * interactionRange);
        Gizmos.DrawRay(playerCamera.transform.position, rightBoundary * interactionRange);
        Gizmos.DrawRay(playerCamera.transform.position, forward * interactionRange);

    }



}
