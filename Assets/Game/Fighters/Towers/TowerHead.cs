using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class TowerHead : NetworkBehaviour 
{
    [SerializeField]
    private GameObject headObject;

    [SerializeField]
    private float speed;
    public float Speed
    { set { speed = value; } }

    [SyncVar]
    private Quaternion networkRotation;

    private TowerCannon cannon;

    [SerializeField]
    private MoveTowardAndHide projectile;

    void Start()
    {
        cannon = GetComponent<TowerCannon>();
    }

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

    public void moveProjectile(Vector3 start, Vector3 end)
    {
        RpcMoveTowardAndHide(start, end);
    }

    [ClientRpc]
    public void RpcMoveTowardAndHide(Vector3 start, Vector3 goal)
    {
        projectile.move(start, goal);
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
