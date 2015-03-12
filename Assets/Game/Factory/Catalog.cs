using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public enum BuySpawn
{
    DEFAULT,            //Value when not initialized
    NOTHING,            //No possible upgrade, for exemple
    BASIC,
    TOWER,
    TOWER2,
    EFFECTEXPLOSION
}

public class Catalog : MonoBehaviour
{
    //Fill dictionnay from inspector
    [Serializable]
    public struct CatalogEntry
    {
        public BuySpawn spawn;
        public GameObject spawnPrefab;
    }

    [SerializeField]
    public List<CatalogEntry> spawns;

    Dictionary<BuySpawn, GameObject> spawnDict;
    public Dictionary<BuySpawn, GameObject> SpawnDict
    {
        get { return spawnDict; }
        set { spawnDict = value; }
    }

	// Use this for initialization
    void Awake()
    {
        spawnDict = new Dictionary<BuySpawn, GameObject>();
        foreach (CatalogEntry e in spawns)
            SpawnDict.Add(e.spawn, e.spawnPrefab);
	
	}
	
    public GameObject getPrefab(BuySpawn e)
    {
        if (SpawnDict.ContainsKey(e))
            return SpawnDict[e];
        return null;
    }

    public bool containsCreep(BuySpawn e)
    {
        if (SpawnDict.ContainsKey(e))
            if (SpawnDict[e].GetComponent<Creep>() != null)
                return true;
        return false;
    }
}
