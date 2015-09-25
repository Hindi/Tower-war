using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class OccupentHolder : NetworkBehaviour
{
    private bool isOccupied = false;
    public bool IsOccupied
    {
        get { return isOccupied; }
        set { isOccupied = value; }
    }

    public GameObject occupent;

    Tile tile;

    private List<GameObject> creepsOnTile;

    public void addOccupent(GameObject occ)
    {
        if (!IsOccupied)
        {
            IsOccupied = true;
            occupent = occ;
            if(isServer && !isClient)
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
        creepsOnTile = new List<GameObject>();
    }

    public bool hasCreepOnIt()
    {
        return (creepsOnTile.Count > 0);
    }

    public void destroyOccupent()
    {
        if (isServer && !isClient)
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

    public void notifyCreepEnter(GameObject obj)
    {
        if (findCreepOnTile(obj) == null)
            return;
        creepsOnTile.Add(obj);
    }

    public void notifyCreepLeave(GameObject obj)
    {
        GameObject objInList = findCreepOnTile(obj);
        if (objInList != null)
            creepsOnTile.Remove(objInList);
    }

    public void notifyCreepDestruction(GameObject obj)
    {
        notifyCreepLeave(obj);
    }

    private GameObject findCreepOnTile(GameObject obj)
    {
        int id = obj.GetComponent<FactoryModel>().Id;
        GameObject result = null;
        creepsOnTile.ForEach(delegate(GameObject g)
        {
            if (g.GetComponent<FactoryModel>().Id == id)
                result = g;
        });
        return result;
    }
}
