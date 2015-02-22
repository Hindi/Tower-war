using UnityEngine;
using System.Collections;

public class OccupentHolder : MonoBehaviour {

    int creepCounter = 0;

    private bool isOccupied = false;
    public bool IsOccupied
    {
        get { return isOccupied; }
        set { isOccupied = value; }
    }

    private GameObject occupent;

    public void addOccupent(GameObject occ)
    {
        occupent = occ;
        IsOccupied = true;
        EventManager.Raise(EnumEvent.TILEMAPUPDATE);
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
