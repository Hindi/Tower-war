using UnityEngine;
using System.Collections;

public class TowerHead : MonoBehaviour 
{
    [SerializeField]
    private GameObject headObject;

    [SerializeField]
    private float speed;

    private Quaternion networkRotation;

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

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(headObject.transform.rotation);
        }
        else
        {
            this.networkRotation = (Quaternion)stream.ReceiveNext();
        }
    }
}
