﻿using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class TowerActivity : Activity
{
    protected override void activate(bool b)
    {
        if (!b)
        {
            hide();
        }

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
        transform.position = position;
    }
}
