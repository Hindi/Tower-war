using UnityEngine;
using System.Collections;

public class OccupentHolder : MonoBehaviour {

    private bool isOccupied = false;
    public bool IsOccupied
    {
        get { return isOccupied; }
        set 
        { 
            if(value)
                EventManager.Raise(EnumEvent.TILEMAPUPDATE);
            isOccupied = value;
        }
    }

    private GameObject occupent;
    public GameObject Occupent
    {
        get { return occupent; }
        set 
        {
            isOccupied = (value != null);
            IsOccupied = value; 
        }
    }

    public void destroyOccupent()
    {

    }
}
