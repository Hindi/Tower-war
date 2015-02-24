using UnityEngine;
using System.Collections;

public class TowerHead : MonoBehaviour 
{
    [SerializeField]
    private GameObject headObject;

    [SerializeField]
    private float speed;

    public void lookAt(Vector3 pos)
    {
        var rot = computeRotation(pos);
        headObject.transform.rotation = Quaternion.Slerp(headObject.transform.rotation, computeRotation(pos), Time.deltaTime * speed);
    }

    private Quaternion computeRotation(Vector3 pos)
    {
        Vector3 norm = (pos - headObject.transform.position);
        return Quaternion.LookRotation(norm, Vector3.forward);
    }
}
