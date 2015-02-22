using UnityEngine;
using System.Collections.Generic;

public enum EnumSpawn
{
    BASIC,
    TOWER
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

    Dictionary<EnumSpawn, Machine> machinesDict;

    [SerializeField]
    private Machine basicPrefab;
    [SerializeField]
    private Machine towerPrefab;

    private int nextId;

    void Start()
    {
        nextId = 0;
        machinesDict = new Dictionary<EnumSpawn, Machine>();
        machinesDict.Add(EnumSpawn.BASIC, basicPrefab);
        machinesDict.Add(EnumSpawn.TOWER, towerPrefab);
    }

    public GameObject spawn(EnumSpawn type, Vector3 position)
    {
        GameObject currentObj = machinesDict[type].createModel(nextId);
        currentObj.transform.position = position;
        currentObj.transform.parent = transform;
        nextId++;
        return currentObj;
    }
}
