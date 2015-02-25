using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreepTargetKeeper : MonoBehaviour {

    List<TowerFocus> towers;

    void Start()
    {
        towers = new List<TowerFocus>();
    }

    public void notifyTarget(TowerFocus focus)
    {
        if (!towers.Contains(focus))
            towers.Add(focus);
    }

    public void notifyExit()
    {
        foreach (TowerFocus t in towers)
            t.excludeTarget(gameObject);
        towers.Clear();
    }
}
