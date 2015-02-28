using UnityEngine;
using System.Collections;

public class TowerActivity : Activity
{
    protected override void activate(bool b)
    {
        if (b)
            GetComponent<Tower>().spawn();
        else
        {
            machine.putAway(gameObject);
            GetComponent<Tower>().notifyDesactivation();
        }
        hide(b);
    }
}
