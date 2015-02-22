using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreepMachine
{
    private GameObject model;
    public GameObject Model
    {
        set { model = value; }
    }

    List<GameObject> inUse;
    List<GameObject> waiting;

    public CreepMachine(GameObject mod)
    {
        Model = mod;
    }

    public GameObject createCreep()
    {
        return (GameObject)GameObject.Instantiate(model);
    }

}
