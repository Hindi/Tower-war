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
            if(machine != null)
                machine.putAway(gameObject);
            GetComponent<CreepMovement>().notifyDesactivation();
            GetComponent<CreepTargetKeeper>().notifyExit();
        }
        GetComponent<CreepMovement>().enabled = b;
        GetComponent<CapsuleCollider>().enabled = b;
        hide(b);
    }
}
