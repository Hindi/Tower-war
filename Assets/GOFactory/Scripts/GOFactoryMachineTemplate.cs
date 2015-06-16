using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// Class for Inspector customization only
/// </summary>
[Serializable()] 
public class GOFactoryMachineTemplate
{
    /// <summary>MachinName exposition for catalog</summary>
    [Tooltip("The name of the machine or the prefab.")]
    public string MachineName = "";

    /// <summary>PreInstantiateCount exposition for catalog</summary>
    [Tooltip("The number of instances spawned on start.")]
    public int PreInstantiateCount;

    /// <summary>Prefab exposition for catalog</summary>
    [Tooltip("If no prefab is set, the factory will instantiate objects from the Resources folders using the machine name.")]
    public GameObject Prefab;

    /// <summary>LifeSpan exposition for catalog</summary>
    [Tooltip("Time before the object is destroyed after it is spawned. 0 = infinite")]
    public int LifeSpan;

    /// <summary>InactiveLifeSpan exposition for catalog</summary>
    [Tooltip("Time before the object is destroyed when recycled. 0 = infinite")]
    public int InactiveLifeSpan;

    /// <summary>DefaultPosition exposition for catalog</summary>
    [Tooltip("The position where the objects are spawned.")]
    public Vector3 DefaultPosition;

    /// <summary>folded exposition for catalog</summary>
    public bool folded = true;

    /// <summary>InstantiationType exposition for catalog</summary>
    [Tooltip("The object is either spawned for local client or the whole nework.")]
    public GOF.InstantiationType InstantiationType;

    /// <summary>Group exposition for catalog</summary>
    [Tooltip("The group for network instantiation.")]
    public int Group = 0;
}
