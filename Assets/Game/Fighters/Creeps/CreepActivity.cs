using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

[RequireComponent(typeof(CreepMovement), typeof(CreepMortality))]
public class CreepActivity : Activity
{
    protected override void activate(bool b)
    {
        if (isServer)
        {
            base.activate(b);
            RpcActivate(b, transform.position);
        }
        if (b)
        {
            GetComponent<CreepMovement>().spawn();
            GetComponent<CreepMortality>().reset();
        }
        else
        {
            GetComponent<CreepMovement>().notifyDesactivation();
            GetComponent<CreepTargetKeeper>().notifyExit();
            hide();
        }
        GetComponent<CreepMovement>().enabled = b;
        GetComponent<CapsuleCollider>().enabled = b;
    }

    protected void hide()
    {
        transform.position = new Vector3(1000, 0, 0);
    }

    [ClientRpc]
    public void RpcActivate(bool b, Vector3 position)
    {
        transform.position = position;
        activate(b);
    }
}
