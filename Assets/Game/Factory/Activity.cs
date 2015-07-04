using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class Activity : NetworkBehaviour
{

    [SerializeField]
    private float inactivityTimeBeforeDestroy;

    [SerializeField]
    protected GameObject model;

    private Machine machine;
    public Machine Machine
    {
        set { machine = value; }
    }

    protected bool isActive;
    public bool Active
    {
        get { return isActive; }
        set
        {
            isActive = value;
            activate(isActive);
        }
    }

    protected virtual void activate(bool b)
    {
        if (!b)
            machine.putAway(gameObject);
    }
}
