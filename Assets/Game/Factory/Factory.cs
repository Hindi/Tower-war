using UnityEngine;
using System;
using System.Collections.Generic;

public enum EnumSpawn
{
    DEFAULT,            //Value when not initialized
    NOTHING,            //No possible upgrade, for exemple
    BASIC,
    TOWER,
    TOWER2,
    EFFECTEXPLOSION
}

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

    //Fill dictionnay from inspector
    [Serializable]
    public struct entry
    {
        public EnumSpawn spawn;
        public Machine machine;
    }

    [SerializeField]
    public List<entry> machines;

    Dictionary<EnumSpawn, Machine> machinesDict;

    private int nextId;

    void Awake()
    {
        nextId = 0;
        machinesDict = new Dictionary<EnumSpawn, Machine>();
        foreach (entry e in machines)
            machinesDict.Add(e.spawn, e.machine);
    }

    void Start()
    {
    }

    public GameObject spawn(EnumSpawn type, Vector3 position)
    {
        GameObject currentObj = machinesDict[type].createModel(nextId, position);
        currentObj.transform.parent = transform;
        nextId++;
        return currentObj;
    }
}
