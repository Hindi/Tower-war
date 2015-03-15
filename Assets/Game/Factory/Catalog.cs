using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Catalog : MonoBehaviour
{

    [SerializeField]
    public List<GameObject> spawns;
    public List<GameObject> Spawns
    {
        get { return spawns; }
        set { spawns = value; }
    }
	
    public GameObject getPrefab(int index)
    {
        if (contains(index))
            return spawns[index];
        return null;
    }

    public bool contains(int index)
    {
        return (spawns.Count > index);
    }
}
