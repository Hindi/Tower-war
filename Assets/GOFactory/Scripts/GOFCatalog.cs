using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Catalog exposed to the inspector
/// </summary>
public class GOFCatalog : MonoBehaviour 
{
    /// <summary>
    /// The list of machines created from the inspector
    /// </summary>
    public List<GOFactoryMachineTemplate> MachinesList;
}
