using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class OccupentHolder : NetworkBehaviour
{

    int creepCounter = 0;

    private bool isOccupied = false;
    public bool IsOccupied
    {
        get { return isOccupied; }
        set { isOccupied = value; }
    }

    public GameObject occupent;

    Tile tile;

    public void addOccupent(GameObject occ)
    {
        if (!IsOccupied)
        {
            IsOccupied = true;
            occupent = occ;
            if(isServer)
            {
                RpcAddOccupent(occ);
                EventManager.Raise(EnumEvent.TILEMAPUPDATE);
            }

            occupent.GetComponent<OccupentTileInfos>().Tile = tile;
            occupent.GetComponent<OccupentTileInfos>().Zone = tile.Zone;
        }
    }

    [ClientRpc]
    private void RpcAddOccupent(GameObject obj)
    {
        addOccupent(obj);
    }

    void Start()
    {
        tile = GetComponent<Tile>();
    }

    public bool hasCreepOnIt()
    {
        return (creepCounter > 0);
    }

    public void destroyOccupent()
    {
        if (isServer)
            RpcDestroyOccupent();
        occupent.GetComponent<Activity>().Active = false;
        IsOccupied = false;
        EventManager.Raise(EnumEvent.TILEMAPUPDATE);
    }

    [ClientRpc]
    private void RpcDestroyOccupent()
    {
        destroyOccupent();
    }

    public bool canBuild()
    {
        return (!hasCreepOnIt() && GetComponent<Tile>().Zone.canBuildHere(GetComponent<Tile>()) && !IsOccupied);
    }

    public void notifyCreepEnter()
    {
        creepCounter++;
    }

    public void notifyCreepLeave()
    {
        creepCounter--;
    }

    public void notifyCreepDestruction()
    {
        creepCounter--;
    }
}
