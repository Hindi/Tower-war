using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

[RequireComponent(typeof(CreepMovement), typeof(CreepMortality))]
public class CreepActivity : Activity
{
    [SerializeField]
    private GameObject fxObject;

    protected override void activate(bool b)
    {
        if (isServer)
        {
            base.activate(b);
            if(!isClient)
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
            showFxAndHide();
        }
        GetComponent<CreepMovement>().enabled = b;
        GetComponent<CapsuleCollider>().enabled = b;
    }

    public void showFxAndHide()
    {
        if (fxObject)
        {
            fxObject.GetComponent<OneShotParticle>().setInactive(hide);
        }
        else
            hide();
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
