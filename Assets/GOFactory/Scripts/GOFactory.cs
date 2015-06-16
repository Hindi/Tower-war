using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("GOFactoryMachine")]
namespace GOF
{
    /// <summary>
    /// The <see cref="GOF"/> namespace contains classes for ....
    /// </summary>

    /// <summary>
    /// Enum that define if a machine works on the network on not.
    /// </summary>
    public enum InstantiationType
    {
        /// <summary>Local instantiation</summary>
        local = 0,
        /// <summary>Networked instantiation</summary>
        network = 1
    }

    /// <summary>
    /// This class is the interface between the user and and the machines. 
    /// </summary>
    public class GOFactory : MonoBehaviour
    {
        /// <summary>
        /// Dictionnary that contains all the existing machines.
        /// </summary>
        private Dictionary<string, GOFactoryMachine> machines;

        /// <summary>
        /// The catalog that allow the user to create macine from the inspector.
        /// </summary>
        [SerializeField]
        private GOFCatalog catalog;
        
        #region public_interface

        /// <summary>
        /// Create a new gameobject (use an inactive one or instantiate one).
        /// If a machine exists with this name but has no prefab attached, the machine will try to instantiate a prefab with this name from the Resources folder.
        /// If no machine exists with this name but a prefab with this name exists in a Resource folder, then a new machine with this name will be created.
        /// If none of this is possible, an error will be displayed and null is returned.
        /// </summary>
        /// <param name="machineName">The machine that spawn these instances.</param>
        /// <param name="position">The position where the gameobject is spawned.</param>
        /// <param name="forceNewSpawn">If true the oject will be Insantiated.</param>
        /// If none of this is possible, an error will be displayed and null is returned.</param>
        /// <returns>The new GameObject.</returns>
        public GameObject spawn(string machineName, Vector3 position, bool forceNewSpawn = false)
        {
            if (machines.ContainsKey(machineName))
                return machines[machineName].createModel(position, forceNewSpawn, false);
            else
                return createMachineAndSpawn(machineName, position, forceNewSpawn);
        }

        /// <summary>
        /// Create a new gameobject (use an inactive one or instantiate one).
        /// If a machine exists with this name but has no prefab attached, the machine will try to instantiate a prefab with this name from the Resources folder.
        /// If no machine exists with this name but a prefab with this name exists in a Resource folder, then a new machine with this name will be created.
        /// If none of this is possible, an error will be displayed and null is returned.
        /// </summary>
        /// <param name="machineName">The machine that spawn these instances.</param>
        /// <param name="forceNewSpawn">If true the oject will be Insantiated.</param>
        /// <returns>The new GameObject.</returns>
        public GameObject spawn(string machineName, bool forceNewSpawn = false)
        {
            if (machines.ContainsKey(machineName))
                return machines[machineName].createModel(forceNewSpawn, false);
            else
                return createMachineAndSpawn(machineName, Vector3.zero, forceNewSpawn);
        }

        /// <summary>
        /// Create a new gameobject (use an inactive one or instantiate one).
        /// If a machine exists with this name but has no prefab attached, the machine will try to instantiate a prefab with this name from the Resources folder.
        /// If no machine exists with this name but a prefab with this name exists in a Resource folder, then a new machine with this name will be created.
        /// If none of this is possible, an error will be displayed and null is returned.
        /// </summary>
        /// <param name="machineName">The machine that spawn these instances.</param>
        /// <param name="forceNewSpawn">If true the oject will be Insantiated.</param>
        /// <returns>The new GameObject.</returns>
        public GameObject spawnRecycled(string machineName)
        {
            if (machines.ContainsKey(machineName))
                return machines[machineName].createModel(true, true);
            else
                return createMachineAndSpawn(machineName, Vector3.zero, true);
        }

        /// <summary>
        /// Create a machine with a name a an optional list of options.
        /// </summary>
        /// <param name="machineName">The name of the machine</param>
        /// <param name="options">As much GOFactoryOption objects as necessary.</param>
        public void createMachine(string machineName, params GOFOption[] options)
        {
            if(machineName != "")
            {
                if (!machines.ContainsKey(machineName))
                {
                    var m = new GOFactoryMachine();
                    m.MachineName = machineName;
                    m.Factory = this;
                    machines.Add(machineName, m);
                    setMachineOptions(m, options);
                }
                else
                {
                    Debug.LogError("[GOF]There is already a machine with this name : " + machineName + ". Please call 'configureMachine' if you want to change its configuration.");
                }
            }
        }

        /// <summary>
        /// Apply new options an existing machine.
        /// </summary>
        /// <param name="machineName">The name of the machine</param>
        /// <param name="options">As much GOFactoryOption objects as necessary.</param>
        public void configureMachine(string machineName, params GOFOption[] options)
        {
            if (machines.ContainsKey(machineName))
                setMachineOptions(machines[machineName], options);
            else
            {
                Debug.LogError("[GOF]You're trying to configure a machine that does not exists : " + machineName + ". Please call 'createMachine' first.");
            }
        }

        /// <summary>
        /// The number of active instance possessed by this machine in the scene.
        /// </summary>
        /// <param name="name">The name of the machine.</param>
        /// <returns>The number of active instances.</returns>
        public int activeInstanceCount(string machineName)
        {
            if (machines.ContainsKey(machineName))
                return machines[machineName].activeInstanceCount();
            else
                Debug.LogError("There is no machine with the name : " + machineName);
            return -1;
        }

        /// <summary>
        /// The number of inactive instance possessed by this machine in the scene.
        /// </summary>
        /// <param name="name">The name of the machine.</param>
        /// <returns>The number of inactive instances.</returns>
        public int inactiveInstanceCount(string machineName)
        {
            if (machines.ContainsKey(machineName))
                return machines[machineName].inactiveInstanceCount();
            else
                Debug.LogError("There is no machine with the name : " + machineName);
            return -1;
        }

        /// <summary>
        /// Destroy all the instances (active and inactive) possessed by this machine.
        /// </summary>
        /// <param name="name">The name of the machine.</param>
        public void destroyAllInstance(string machineName)
        {
            if (machines.ContainsKey(machineName))
                machines[machineName].clear();
            else
                Debug.LogError("There is no machine with the name : " + machineName);
        }

        /// <summary>
        /// Recycle all the instances (active and inactive) possessed by this machine.
        /// </summary>
        /// <param name="name">The name of the machine.</param>
        public void recycleAllInstance(string machineName)
        {
            if (machines.ContainsKey(machineName))
                machines[machineName].recycleAll();
            else
                Debug.LogError("There is no machine with the name : " + machineName);
        }

        /// <summary>
        /// Destroy all the instances.
        /// </summary>
        public void destroyAllInstance()
        {
            foreach(KeyValuePair<string, GOFactoryMachine> p in  machines)
                p.Value.clear();
        }

        /// <summary>
        /// Recycle all the instances.
        /// </summary>
        public void recycleAllInstance()
        {
            foreach (KeyValuePair<string, GOFactoryMachine> p in machines)
                p.Value.recycleAll();
        }

        /// <summary>
        /// Instantiate gameobjects and put them in the waiting list for later use.
        /// </summary>
        /// <param name="machine">The machine that have to spawn the gameobjects.</param>
        /// <param name="count">The number of instances to create.</param>
        public void preInstantiate(string machine, int count)
        {
            for (int i = 0; i < count; ++i)
                spawnRecycled(machine);
        }

        /// <summary>
        /// Recycle a specific object.
        /// </summary>
        /// <param name="obj">The gameobject to recycle.</param>
        public static void Recycle(GameObject obj)
        {
            obj.GetComponent<GOFTracker>().recycle();
        }

        /// <summary>
        /// Recycle a specific object.
        /// </summary>
        /// <param name="obj">The gameobject to recycle.</param>
        public static void Recycle(GOFTracker objTracker)
        {
            objTracker.recycle();
        }

        #endregion

        #region private

        /// <summary>
        /// Load the catalog.
        /// </summary>
        void Awake()
        {
            machines = new Dictionary<string, GOFactoryMachine>();
            loadCatalog();
        }

        /// <summary>
        /// If a prefab of this name exists in the Resources folder, then a machine is created and its prefab is loaded from the Resources folder.
        /// </summary>
        /// <param name="machineName">The name of the machine</param>
        /// <param name="position">The position where the gameobject is spawned.</param>
        /// <param name="forceNewSpawn">If true the oject will be Insantiated.</param>
        /// <param name="startRecycled">If true the oject will be recycled immediatly.</param>
        /// <returns></returns>
        private GameObject createMachineAndSpawn(string machineName, Vector3 position, bool forceNewSpawn, bool startRecycled = false)
        {
            GameObject obj = Resources.Load(machineName) as GameObject;
            if (obj != null)
            {
                createMachine(machineName);
                return machines[machineName].createModel(position, forceNewSpawn, startRecycled);
            }
            Debug.LogError("[GOF]Couldn't find any prefab named " + machineName + ". Returning null.");
            return null;
        }

        /// <summary>
        /// Genrate the unique id for the instances.
        /// </summary>
        /// <returns>A unique id.</returns>
        internal string generateId()
        {
            return Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Used by the machines to start coroutines.
        /// </summary>
        /// <param name="coroutineMethod">The coroutine to start.</param>
        internal void startChildCoroutine(IEnumerator coroutineMethod)
        {
            StartCoroutine(coroutineMethod);
        }

        /// <summary>
        /// Create the machines listed in the catalog.
        /// </summary>
        private void loadCatalog()
        {
            if(catalog != null)
            {
                foreach (GOFactoryMachineTemplate t in catalog.MachinesList)
                {
                    createMachine(t.MachineName, GOFOption.Prefab(t.Prefab), GOFOption.LifeSpan(t.LifeSpan),
                        GOFOption.InactiveLifeSpan(t.InactiveLifeSpan), GOFOption.Position(t.DefaultPosition),
                        GOFOption.PreInstantiate(t.PreInstantiateCount), GOFOption.Network(t.Group, (t.InstantiationType == InstantiationType.network)));
                }
            }
        }

        /// <summary>
        /// Apply a list of options to the machine. Pre instantiation is always done last.
        /// If configure is called with the PreInstantiate option, the machine will instantiate more ocjects.
        /// </summary>
        /// <param name="m">The machine</param>
        /// <param name="options">The option list</param>
        private void setMachineOptions(GOFactoryMachine m, params GOFOption[] options)
        {
            int preInstant = 0;
            foreach (GOFOption option in options)
            {
                switch(option.type)
                {
                    case GOFactoryOptionEnum.position:
                        m.DefaultPos = option.v3;
                        break;
                    case GOFactoryOptionEnum.prefab:
                        m.Prefab = option.obj;
                        break;
                    case GOFactoryOptionEnum.inactiveLifeSpan:
                        m.InactiveLifeSpan = option.f;
                        break;
                    case GOFactoryOptionEnum.lifeSpan:
                        m.LifeSpan = option.f;
                        break;
                    case GOFactoryOptionEnum.preInstantiate:
                        preInstant = option.i;
                        break;
                    case GOFactoryOptionEnum.network:
                        m.Group = option.i;
                        m.Networked = option.b;
                        break;
                    default:
                        break;
                }
            }
            if (preInstant > 0)
                preInstantiate(m.MachineName, preInstant);
        }

        
        
        #endregion
    }

}
