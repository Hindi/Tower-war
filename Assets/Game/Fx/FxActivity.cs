using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class FxActivity : Activity
{
    protected override void activate(bool b)
    {
        if (!b)
        {
            hide();
        }

        Debug.Log(transform.position);
        if (isServer)
        {
            base.activate(b);
            RpcActivate(transform.position);
        }
    }

    protected void hide()
    {
        transform.position = new Vector3(1000, 0, 0);
    }

    [ClientRpc]
    public void RpcActivate(Vector3 position)
    {
        Debug.Log(position);
        transform.position = position;
    }
}
