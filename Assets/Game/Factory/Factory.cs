using UnityEngine;
using System;
using System.Collections.Generic;

public class Factory : MonoBehaviour
{
    private static Factory instance;

    public static Factory Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<Factory>();
            return instance;
        }
    }

    [SerializeField]
    Catalog catalog;

    Dictionary<BuySpawn, Machine> machinesDict;

    private int nextId;

    void Start()
    {
        nextId = 0;
        machinesDict = new Dictionary<BuySpawn, Machine>();
        foreach (KeyValuePair<BuySpawn,GameObject> p in catalog.SpawnDict)
        {
            Machine machine = new Machine();
            machine.ModelName = p.Value.name;
            machinesDict.Add(p.Key, machine);
        }
    }

    public GameObject spawn(BuySpawn type, Vector3 position)
    {
        GameObject currentObj = machinesDict[type].createModel(nextId, position);
        currentObj.transform.parent = transform;
        nextId++;
        return currentObj;
    }
}
