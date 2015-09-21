using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(CreepMovement), typeof(CreepMortality))]
public class CreepActivity : Activity
{
    [SerializeField]
    private GameObject fxObject;

    [SerializeField]
    private List<Transform> raycastOrigins;

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
            notifyDesactivation();
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

    private void notifyDesactivation()
    {
        foreach(Transform t in raycastOrigins)
        {
            RaycastHit2D[] hits;
            hits = Physics2D.RaycastAll(t.position, -t.right, 1.0f);
            foreach (RaycastHit2D hit in hits)
                if (hit.collider.tag == "Tile")
                    hit.collider.GetComponent<OccupentHolder>().notifyCreepDestruction(gameObject);
        }
    }
}
