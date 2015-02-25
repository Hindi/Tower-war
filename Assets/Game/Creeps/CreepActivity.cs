using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CreepMovement), typeof(CreepMortality))]
public class CreepActivity : Activity 
{
    
    protected override void activate(bool b)
    {
        if (b)
        {
            GetComponent<CreepMovement>().spawn();
            GetComponent<CreepMortality>().reset();
        }
        else
        {
            machine.putAway(gameObject);
            GetComponent<CreepMovement>().notifyDesactivation();
            GetComponent<CreepTargetKeeper>().notifyExit();
        }
    }
}
