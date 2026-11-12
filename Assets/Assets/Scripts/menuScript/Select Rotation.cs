using UnityEngine;

public class SelectRotation : MonoBehaviour
{
    public Transform aguja;
    public float speedRotation = 1;
    public float newZ;

    public void GotoRotationZ(float z)
    {
        newZ = z;
    }

    public void Update()
    {
        Vector3 euler = aguja.localEulerAngles;
        euler.z = Mathf.LerpAngle(euler.z, newZ, speedRotation * Time.deltaTime);
        aguja.localEulerAngles = euler;
    }




}
