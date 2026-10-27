using UnityEngine;

public class TriggerBattle : MonoBehaviour
{
    [SerializeField] private float sphereRadius = 3;
    [SerializeField] private Vector3 offset = new Vector3(05f, -0.5f, 0f);
    public bool inZone;
    public LayerMask mask;


    // Update is called once per frame
    void Update()
    {
        inZone = Physics.CheckSphere(transform.position + offset, sphereRadius, mask);
            
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.indianRed;
        Gizmos.DrawWireSphere(transform.position + offset, sphereRadius);
    }


}
