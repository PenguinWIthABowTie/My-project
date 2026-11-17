using System.Collections;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    //La palabra protected funciona como privat pero si que da acceso a los scripts que heredan de este
    //Referencia al collider para poder activarlo y desactivarlo
    protected Collider col;
    protected Rigidbody rb;   


    protected void Start()
    {
        col = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
    }

    //Esta funcion se llama por el PickupDetector. Tiene que ser publica o no dara acceso
    //Añadimos VIRTUAL para que se le pueda hacer OVERRIDE desde las clases que hereden de este
    public virtual void PickUp(Transform point)
    {
        //Emparentar el objeto a la mano del jugador
        transform.SetParent(point);
        //Para moverlo a la posision exacta del padre hay que modificar su LOCAL POSITION
        transform.localPosition = Vector3.zero;
        //Desactivar la collision deel objeto
        col.enabled = false;
        //Cambiar el rigidbody a Kinematic para que se este quieto
        rb.isKinematic = true;
        //Cambiar la capa del objeto para que se renderice por encima de todo lo demas (como el brazo)
        gameObject.layer = LayerMask.NameToLayer("FPSArm");
    }

    public void Drop()
    {
        //Desemparentar el objeto
        transform.SetParent(null);
        //Reactivar el collider
        col.enabled = true;
        //Quitar el Kinematic al rigidbody para que vuelva a tener fisicas
        rb.isKinematic = false;
        //Devolver la capa a su valor original (Pickup) para que se puede coger el objeto otra vez
        gameObject.layer = LayerMask.NameToLayer("Pickup");
    }

    public void Throw(Vector3 force)
    {
        //Soltar el objeto antes de lanzarlo
        Drop();
        //Aplicar la fuerza que mande el jugador
        rb.AddForce(force, ForceMode.VelocityChange);
    }

}
