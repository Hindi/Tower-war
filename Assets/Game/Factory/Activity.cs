using UnityEngine;
using System.Collections;

public abstract class Activity : MonoBehaviour {

    protected bool active;
    public bool Active
    {
        get { return active; }
        set
        {
            active = value;
            activate(active);
        }
    }

    protected Machine machine;
    public Machine Machine
    {
        set { machine = value; }
    }

    protected abstract void activate(bool b);

}
