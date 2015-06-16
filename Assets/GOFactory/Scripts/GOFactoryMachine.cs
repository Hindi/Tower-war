using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace GOF
{
    /// <summary>
    /// The machines manage the instances creation, recycling and destruction.
    /// </summary>
    public class GOFactoryMachine
    {
        #region properties
        /// <summary>
        /// The name of the machine. It must be  unique.
        /// </summary>
        private string machineName;
        /// <summary>machineName accessor</summary>
        public string MachineName
        {
            set { machineName = value; }
            get { return machineName; }
        }

        /// <summary>
        /// The prefab that will be instantiated.
        /// </summary>
        private GameObject prefab;
        /// <summary>prefab accessor</summary>
        public GameObject Prefab
        {
            set { prefab = value; }
        }

        /// <summary>
        /// This dictionnary contains the instances that are currently used.
        /// </summary>
        private Dictionary<string, GameObject> inUse;

        /// <summary>
        /// This list contains the instances that are desactivated and wait for being used.
        /// </summary>
        private List<GameObject> waiting;

        /// <summary>
        /// Reference o the factory.
        /// </summary>
        private GOFactory factory;
        /// <summary>factory accessor</summary>
        public GOFactory Factory
        { set { factory = value; } }

        /// <summary>
        /// Thep osition where the instances are spawned.
        /// </summary>
        private Vector3 defaultPos;
        /// <summary>defaultPos accessor</summary>
        public Vector3 DefaultPos
        { set { defaultPos = value; } }

        /// <summary>
        /// The time before an inactive instance is destroyed. 0 = infinite.
        /// </summary>
        private float inactiveLifeSpan;
        /// <summary>inactiveLifeSpan accessor</summary>
        public float InactiveLifeSpan
        { set { inactiveLifeSpan = value; } }

        /// <summary>
        /// The time before an active instance is recycled. 0 = infinite.
        /// </summary>
        private float lifeSpan;
        /// <summary>lifeSpan accessor</summary>
        public float LifeSpan
        { set { lifeSpan = value; } }

        /// <summary>
        /// The network group used when network instantiation is activated.
        /// </summary>
        private int group;
        /// <summary>group accessor</summary>
        public int Group
        { set { group = value; } }

        /// <summary>
        /// True if instantiation is activated.
        /// </summary>
        private bool networked;
        /// <summary>networked accessor</summary>
        public bool Networked
        { set { networked = value; } }
        #endregion

        /// <summary>
        /// Accessor to inUse count property.
        /// </summary>
        /// <returns>An int, the number of active instances.</returns>
        public int activeInstanceCount()
        {
            return inUse.Count;
        }

        /// <summary>
        /// Accessor to waiting count property.
        /// </summary>
        /// <returns>An int, the number of inactive instances.</returns>
        public int inactiveInstanceCount()
        {
            return waiting.Count;
        }

        /// <summary>
        /// Default constructor that initialize the member variables.
        /// </summary>
        public GOFactoryMachine()
        {
            inactiveLifeSpan = 0;
            lifeSpan = 0;
            defaultPos = new Vector3(0, 0, 0);
            inUse = new Dictionary<string, GameObject>();
            waiting = new List<GameObject>();
        }

        /// <summary>
        /// Create an instance with the default position.
        /// </summary>
        /// <param name="forceNewSpawn">If true the machine won't pick an object in the recycled instances.</param>
        /// <param name="startRecycled">If true the create object will be recycled.</param>
        /// <returns>The instantiated gameobject</returns>
        public GameObject createModel(bool forceNewSpawn, bool startRecycled)
        {
            return createModel(defaultPos, forceNewSpawn, startRecycled);
        }

        /// <summary>
        /// Create a model at the given position. If a gameobject is currently waiting to be used, it will be used. If not, this machine instantiate a new gameobject.
        /// </summary>
        /// <param name="position">The position where the object will be spawned.</param>
        /// <param name="forceNewSpawn">If true the machine won't pick an object in the recycled instances.</param>
        /// <param name="startRecycled">If true the create object will be recycled.</param>
        /// <returns>The instantiated gameobject</returns>
        public GameObject createModel(Vector3 position, bool forceNewSpawn, bool startRecycled)
        {
            GameObject obj = null;
            if (!forceNewSpawn && waiting.Count > 0)
            {
                obj = waiting[0];
                waiting.Remove(obj);
                obj.transform.position = position;
            }
            else
            {
                if (prefab == null)
                    prefab = Resources.Load(machineName) as GameObject;
                if (prefab != null)
                {
                    if (networked)
                        obj = (GameObject)Network.Instantiate(prefab, position, Quaternion.identity, group);
                    else
                        obj = (GameObject)GameObject.Instantiate(prefab, position, Quaternion.identity);
                }
                else
                {
                    Debug.LogError("[GOF]Couldn't find any prefab named " + machineName + ". Returning null.");
                    return null;
                }

                obj.name = prefab.name;
                obj.transform.SetParent(factory.transform);

                GOFTracker tracker = obj.AddComponent<GOFTracker>();
                tracker.id = factory.generateId();
                tracker.Machine = this;
            }

            obj.SetActive(true);
            inUse.Add(obj.GetComponent<GOFTracker>().Id, obj);

            if (startRecycled)
                GOFactory.Recycle(obj.GetComponent<GOFTracker>());
            else if (lifeSpan > 0)
                factory.startChildCoroutine(activeLifeSpanCoroutine(obj.GetComponent<GOFTracker>(), lifeSpan));

            return obj;
        }

        /// <summary>
        /// Desactivate the object.
        /// </summary>
        /// <param name="obj">The recycled gameobject.</param>
        public void recycle(GOFTracker obj)
        {
            if (obj != null)
            {
                if (inUse.ContainsKey(obj.Id))
                {
                    var creepInList = inUse[obj.Id];
                    inUse.Remove(obj.Id);
                    waiting.Add(creepInList);
                    if (inactiveLifeSpan > 0)
                        factory.startChildCoroutine(inactiveLifeSpanCoroutine(obj, inactiveLifeSpan));
                }
            }
        }

        /// <summary>
        /// Destroy the gameobject.
        /// </summary>
        /// <param name="obj">Gameobject to be destroyed</param>
        public void remove(GOFTracker obj)
        {
            forgetInstance(obj);
            GameObject.Destroy(obj.gameObject);
        }

        /// <summary>
        /// Destroy all the existing instances possessed by this machine (active & inactive).
        /// </summary>
        public void clear()
        {
            foreach (GameObject g in waiting)
                GameObject.Destroy(g);
            foreach (KeyValuePair<string, GameObject> p in inUse)
                GameObject.Destroy(p.Value);
            waiting.Clear();
            inUse.Clear();
        }

        /// <summary>
        /// Recycle all the active instances possessed by this machine.
        /// </summary>
        public void recycleAll()
        {
            foreach (KeyValuePair<string, GameObject> p in inUse.Reverse())
                GOFactory.Recycle(p.Value);
        }

        /// <summary>
        /// Remove the instance references from the lists. This is already called by the OnDestroy function of GOFtracker.
        /// </summary>
        /// <param name="obj">Gameobject to be forgotten</param>
        public void forgetInstance(GOFTracker obj)
        {
            if (waiting.Contains(obj.gameObject))
                waiting.Remove(obj.gameObject);
            if (inUse.ContainsKey(obj.Id))
                inUse.Remove(obj.Id);
        }

        #region coroutines
        /// <summary>
        /// Coroutine called to recycle the gameobject when its lifespan come to the end.
        /// If it is recycled before its time come, this coroutine is canceled.
        /// </summary>
        /// <param name="objTracker">The object to recycle.</param>
        /// <param name="time">The time before it is recycled.</param>
        /// <returns>An IEnumerator, this is a coroutine.</returns>
        IEnumerator activeLifeSpanCoroutine(GOFTracker objTracker, float time)
        {
            float coroutineStartTime = Time.time;
            while (objTracker != null && objTracker.gameObject.activeSelf)
            {
                if (Time.time - coroutineStartTime > time)
                {
                    GOFactory.Recycle(objTracker);
                    break;
                }
                yield return new WaitForEndOfFrame();
            }
        }

        /// <summary>
        /// Coroutine called to recycle the gameobject if its stays inactive ofr the given time.
        /// </summary>
        /// <param name="objTracker">The object to destroy.</param>
        /// <param name="time">The time before it is recycled.</param>
        /// <returns>An IEnumerator, this is a coroutine.</returns>
        IEnumerator inactiveLifeSpanCoroutine(GOFTracker objTracker, float time)
        {
            float coroutineStartTime = Time.time;
            while (objTracker != null && !objTracker.gameObject.activeSelf)
            {
                if (Time.time - coroutineStartTime > time)
                {
                    remove(objTracker);
                    break;
                }
                yield return new WaitForEndOfFrame();
            }
        }
        #endregion
    }
}