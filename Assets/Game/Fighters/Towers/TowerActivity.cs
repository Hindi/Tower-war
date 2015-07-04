using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class TowerActivity : Activity
{
    protected override void activate(bool b)
    {
        if (isServer)
        {
            base.activate(b);
            RpcActivate(b);
        }

        if (!b)
        {
            hide();
        }
    }

    protected void hide()
    {
        transform.position = new Vector3(1000, 0, 0);
    }

    [ClientRpc]
    public void RpcActivate(bool b)
    {
        activate(b);
    }
}
