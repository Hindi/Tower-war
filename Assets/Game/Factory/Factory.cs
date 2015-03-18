﻿using UnityEngine;
using System;
using System.Collections.Generic;

public class Factory : MonoBehaviour
{

    [SerializeField]
    Catalog catalog;

    Dictionary<int, Machine> machinesDict;

    private int nextId;

    void Start()
    {
        nextId = 0;
        machinesDict = new Dictionary<int, Machine>();
        spawnMachines();
    }

    private void spawnMachines()
    {
        for (int i = 0; i < catalog.Spawns.Count; ++i)
        {
            Machine machine = new Machine();
            machine.ModelName = catalog.getPrefab(i).name;
            machinesDict.Add(i, machine);
        }
    }

    public GameObject spawn(int index, Vector3 position)
    {
        GameObject currentObj = machinesDict[index].createModel(nextId, position);
        currentObj.transform.parent = transform;
        nextId++;
        return currentObj;
    }

    public void upgrade(Catalog newCatalog)
    {
        catalog = newCatalog;
        machinesDict.Clear();
        spawnMachines();
    }
}
