using UnityEngine;
using System.Collections;

public class TowerActivity : Activity
{
    protected override void activate(bool b)
    {
        hide(b);
        if (b)
            GetComponent<Tower>().spawn();
        else
        {
            GOF.GOFactory.Recycle(gameObject);
            GetComponent<Tower>().notifyDesactivation();
        }
    }
}
