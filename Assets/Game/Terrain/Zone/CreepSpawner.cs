﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Pathfinder))]
public class CreepSpawner : MonoBehaviour {

    private Pathfinder pathfinder;

    private GameObject startTile;
    public GameObject StartTile
    {
        get { return startTile; }
        set { startTile = value; }
    }

    private GameObject endTile;
    public GameObject EndTile
    {
        get { return endTile; }
        set { endTile = value; }
    }

    [SerializeField]
    private Factory factory;
    public Factory Factory
    { set { factory = value; } }

    PhotonView photonView;

	// Use this for initialization
	void Start () {
        pathfinder = GetComponent<Pathfinder>();
        photonView = GetComponent<PhotonView>();

        EventManager.AddListener(EnumEvent.START, onGameStart);
	}

    [RPC]
    public void spawnRPC(int unit)
    {
        spawn((EnumSpawn)unit);
    }

    private void spawn(EnumSpawn unit)
    {
        GameObject creep = factory.spawn(unit, startTile.transform.position);
        creep.GetComponent<CreepMovement>().Path = pathfinder.Result;
        creep.GetComponent<CreepMovement>().Pathfinder = pathfinder;
    }

    public void requestSpawn(EnumSpawn unit)
    {
        if (PhotonNetwork.offlineMode)
            spawn(unit);
        else
            photonView.RPC("spawnRPC", PhotonTargets.Others, (int)unit);
    }

    void onGameStart()
    {
        updatePath();
    }

    void updatePath()
    {
        pathfinder.StartId = startTile.GetComponent<Tile>().Id;
        pathfinder.GoalId = endTile.GetComponent<Tile>().Id;
        pathfinder.findPath();
    }
}
