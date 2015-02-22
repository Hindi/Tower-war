using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CreepMovement))]
public class CreepActivity : Activity 
{
    
    protected override void activate(bool b)
    {
        if (b)
            GetComponent<CreepMovement>().spawn();
        else
        {
            machine.putAway(gameObject);
            GetComponent<CreepMovement>().notifyDesactivation();
        }
    }
}
