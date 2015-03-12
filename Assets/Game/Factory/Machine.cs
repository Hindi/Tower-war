using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Machine
{
    private string modelName;
    public string ModelName
    {
        get { return modelName; }
        set { modelName = value; }
    }

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
            model.SetActive(true);
            model.transform.position = position;
            model.GetComponent<Activity>().Active = true;
        }
        else
        {
            model = PhotonNetwork.Instantiate(modelName, position, Quaternion.identity, 0);
            model.GetComponent<FactoryModel>().Id = id;
            model.GetComponent<Activity>().Machine = this;
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
        PhotonNetwork.Destroy(obj);
    }
}
