using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class TowerHead : NetworkBehaviour 
{
    [SerializeField]
    private GameObject headObject;

    [SerializeField]
    private float speed;

    [SyncVar]
    private Quaternion networkRotation;

    [SerializeField]
    private TowerCannon cannon;

    public void lookAt(Vector3 pos)
    {
        headObject.transform.rotation = Quaternion.Slerp(headObject.transform.rotation, computeRotation(pos), Time.deltaTime * speed);
    }

    public void networkLookAt()
    {
        headObject.transform.rotation = Quaternion.Slerp(headObject.transform.rotation, networkRotation, Time.deltaTime * 10);
    }

    private Quaternion computeRotation(Vector3 pos)
    {
        Vector3 norm = (pos - headObject.transform.position);
        return Quaternion.LookRotation(norm, Vector3.forward);
    }

    private void Update()
    {
        if(isServer)
        {
            networkRotation = headObject.transform.rotation;
            cannon.tryShot();
        }
    }
}
