using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum EnumCreep
{
    BASIC
}

public class CreepFactory : MonoBehaviour {

    Dictionary<EnumCreep, CreepMachine> machinesDict;

    [SerializeField]
    private GameObject basicPrefab;

    void Start()
    {
        machinesDict = new Dictionary<EnumCreep, CreepMachine>();
        machinesDict.Add(EnumCreep.BASIC, new CreepMachine(basicPrefab));
    }

    public GameObject spawnCreep(EnumCreep type, Vector3 position)
    {
        GameObject currentCreep = machinesDict[type].createCreep();
        currentCreep.transform.position = position;
        currentCreep.transform.parent = transform;
        return currentCreep;
    }
}
