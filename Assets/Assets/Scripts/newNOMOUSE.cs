using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class newNOMOUSE : MonoBehaviour
{
    public GameObject ultimoSeleccionado;
    public GameObject actualSeleccionado;
    

    void Update()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        actualSeleccionado = EventSystem.current.currentSelectedGameObject;


        if (actualSeleccionado != null && actualSeleccionado != ultimoSeleccionado)
        {
           
            if (actualSeleccionado.GetComponent<Button>() != null)
            {
                ultimoSeleccionado = actualSeleccionado;
              
            }
        }

      
        if (actualSeleccionado == null && ultimoSeleccionado != null)
        {
            Debug.Log(ultimoSeleccionado);
         
            EventSystem.current.SetSelectedGameObject(ultimoSeleccionado);
           
        }
    }
}
