using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable()] 
public class Pair
{
    public string Name;
    public float Value;

    public Pair(string n, float second)
    {
        Name = n;
        Value = second;
    }

    public override bool Equals(System.Object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        Pair p = (Pair)obj;
        return Name == p.Name;
    }

    public override int GetHashCode()
    {
        int hashcode = 0;
        hashcode += Name.GetHashCode();
        hashcode += Value.GetHashCode();

        return hashcode;
    }
}

public class FighterStat : MonoBehaviour
{
    [HideInInspector]
    public List<Pair> Attributes;

    void Awake()
    {
        Attributes = new List<Pair>();
        dispatchStats();
    }

    protected virtual void dispatchStats()
    {

    }
}
