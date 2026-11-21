using UnityEngine;

public class QTEScript : MonoBehaviour
{

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {



            Destroy(this.gameObject);
        }
    }
}
