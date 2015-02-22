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

    public void addOccupen(GameObject occ)
    {
        occupent = occ;
        IsOccupied = true;
        EventManager.Raise(EnumEvent.TILEMAPUPDATE);
    }

    public bool canBuild()
    {
        return (!isOccupied && creepCounter == 0);
    }

    public void destroyOccupent()
    {
        Destroy(occupent);
        IsOccupied = false;
        EventManager.Raise(EnumEvent.TILEMAPUPDATE);
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
