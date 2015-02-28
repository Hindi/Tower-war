using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(CreepActivity))]
public class CreepMovement : MonoBehaviour
{
    private List<Vector3> path;
    public List<Vector3> Path
    {
        set
        {
            path = value;
            if (path.Count > 0)
            {
                currentPositionId = 0;
                setNextPosition(currentPositionId);
            }
        }
    }

    private Pathfinder pathfinder;
    public Pathfinder Pathfinder
    {
        set { pathfinder = value; }
    }

    private Vector3 nextPosition;
    private Quaternion goalRotation;
    private Vector3 direction;
    private int currentPositionId;

    [SerializeField]
    private float speed;

    PhotonView photonView;

    private Vector3 networkPosition;

    void Start()
    {
        photonView = GetComponent<PhotonView>();
        if (photonView.isMine)
            EventManager.AddListener(EnumEvent.TILEMAPUPDATE, onMapUpdate);
    }
    
    public void spawn()
    {
        path = pathfinder.Result;
    }

    void onMapUpdate()
    {
        try
        {
            if(gameObject.activeSelf)
                refreshPath(getCurrentTile().Id);
        }
        catch
        {
            Debug.LogError("getCurrentTile from CreepMovement probably failed, returning a null Tile.");
        }
    }

    Tile getCurrentTile()
    {
        RaycastHit2D[] hits;
        hits = Physics2D.RaycastAll(transform.position, -transform.right, 1.0f);
        foreach (RaycastHit2D hit in hits)
            if (hit.collider.tag == "Tile")
                return hit.collider.GetComponent<Tile>();
        return pathfinder.getStartTile();
    }

    void Update()
    {
        if(photonView.isMine)
        {
            transform.position = Vector3.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, nextPosition) < 0.1f)
            {
                if (path.Count == 0 || nextPosition == path[path.Count - 1])
                {
                    GetComponent<CreepActivity>().Active = false;
                }
                else
                    setNextPosition(currentPositionId++);
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, networkPosition) > 1)
                transform.position = networkPosition;
            else
                transform.position = Vector3.Lerp(transform.position, networkPosition, Time.deltaTime * 10);
        }
        transform.rotation = Quaternion.Slerp(transform.rotation, goalRotation, Time.deltaTime * 10);
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            this.networkPosition = (Vector3)stream.ReceiveNext();
            this.goalRotation = (Quaternion)stream.ReceiveNext();
        }
    }

    void OnDestroy()
    {
        EventManager.RemoveListener(EnumEvent.TILEMAPUPDATE, onMapUpdate);
    }

    void refreshPath(int tileId)
    {
        Path = pathfinder.findPathFromPosition(tileId);
    }

    void setNextPosition(int posId)
    {
        if (path.Count == 0)
            nextPosition = transform.position;
        else
            nextPosition = path[posId];
        goalRotation = Quaternion.LookRotation(Vector3.Normalize(transform.position - nextPosition));
    }

    public void notifyDesactivation()
    {
        getCurrentTile().GetComponent<OccupentHolder>().notifyCreepDestruction();
    }
}
