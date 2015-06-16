﻿using UnityEngine;
using System.Collections;

public class Activity : MonoBehaviour
{
    PhotonView photonView;

    [SerializeField]
    private float inactivityTimeBeforeDestroy;

    [SerializeField]
    protected GameObject model;

    protected bool isActive;
    public bool Active
    {
        get { return isActive; }
        set
        {
            isActive = value;
            activate(isActive);
        }
    }

    protected virtual void activate(bool b)
    {
        hide(b);
    }

    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    protected void hide(bool b)
    {
        transform.position = new Vector3(1000, 0, 0);
        if(photonView.isMine)
        {
            photonView.RPC("hideRPC", PhotonTargets.Others, b);
            photonView.RPC("moveRPC", PhotonTargets.Others, b);
        }
    }

    [RPC]
    public void hideRPC(bool b)
    {
        hide(b);
    }
    [RPC]
    public void moveRPC(bool b)
    {
        transform.position = new Vector3(1000, 0, 0);
    }
}
