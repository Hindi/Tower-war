using UnityEngine;
using System.Collections;

public class OccupentHolder : MonoBehaviour
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
            EventManager.Raise(EnumEvent.TILEMAPUPDATE);
        }
        occupent = occ;

        occupent.GetComponent<OccupentTileInfos>().Tile = tile;
        occupent.GetComponent<OccupentTileInfos>().Zone = tile.Zone;
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
        occupent.GetComponent<Activity>().Active = false;
        IsOccupied = false;
        EventManager.Raise(EnumEvent.TILEMAPUPDATE);
    }

    public bool canBuild()
    {
        //Debug.Log(!hasCreepOnIt() + " " + GetComponent<Tile>().Zone.canBuildHere(GetComponent<Tile>())  )
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
