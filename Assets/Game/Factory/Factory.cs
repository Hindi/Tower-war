using UnityEngine;
using System.Collections.Generic;

public enum EnumSpawn
{
    BASIC,
    TOWER,
    TOWER2
}

public class Factory : MonoBehaviour
{

    Dictionary<EnumSpawn, Machine> machinesDict;

    [SerializeField]
    private Machine basicPrefab;
    [SerializeField]
    private Machine towerPrefab;
    [SerializeField]
    private Machine tower2Prefab;

    private int nextId;

    void Start()
    {
        nextId = 0;
        machinesDict = new Dictionary<EnumSpawn, Machine>();
        machinesDict.Add(EnumSpawn.BASIC, basicPrefab);
        machinesDict.Add(EnumSpawn.TOWER, towerPrefab);
        machinesDict.Add(EnumSpawn.TOWER2, tower2Prefab);
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
