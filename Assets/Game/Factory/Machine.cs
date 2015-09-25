using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class Machine : NetworkBehaviour
{
    private string modelName;
    public string ModelName
    {
        get { return modelName; }
        set { modelName = value; }
    }

    private GameObject prefab;

    List<GameObject> inUse;
    List<GameObject> waiting;

    public Machine()
    {
        inUse = new List<GameObject>();
        waiting = new List<GameObject>();
    }

    public GameObject createModel(int id, Vector3 position)
    {
        GameObject model;
        if(waiting.Count > 0)
        {
            model = waiting[0];
            waiting.Remove(model);

            //Recursive call in case this object is null
            if(model == null)
                return createModel(id, position);

            model.SetActive(true);
            model.transform.position = position;
            model.GetComponent<Activity>().Active = true;
        }
        else
        {
            if (prefab == null)
                prefab = Resources.Load(modelName) as GameObject;
            if (prefab == null)
            {
                Debug.LogError("Couldn't find prefab " + modelName + " in the Resource folders.");
                return null;
            }
            model = (GameObject)Instantiate(prefab, position, Quaternion.identity);
            model.GetComponent<Activity>().Machine = this;
            model.GetComponent<FactoryModel>().Id = id;
            NetworkServer.Spawn(model);
        }
        inUse.Add(model);
        return model;
    }

    public void putAway(GameObject obj)
    {
        var creepInList = inUse.Find(c => c.GetComponent<FactoryModel>().Id == obj.GetComponent<FactoryModel>().Id);
        inUse.Remove(creepInList);
        waiting.Add(creepInList);
    }

    public void remove(GameObject obj)
    {
        if (waiting.Contains(obj))
            waiting.Remove(obj);
        if (inUse.Contains(obj))
            inUse.Remove(obj);
    }
}
