using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Machine : MonoBehaviour
{
    [SerializeField]
    private string modelName;

    List<GameObject> inUse;
    List<GameObject> waiting;

    [SerializeField]
    private float inactivityTimeBeforeDestroy;

    void Start()
    {
        inUse = new List<GameObject>();
        waiting = new List<GameObject>();
    }

    public GameObject createModel(int id)
    {
        GameObject model;
        if(waiting.Count > 0)
        {
            model = waiting[0];
            waiting.Remove(model);
            model.SetActive(true);
            model.GetComponent<Activity>().Active = true;
        }
        else
        {
            model = PhotonNetwork.Instantiate(modelName, Vector3.zero, Quaternion.identity, 0);
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
        creepInList.SetActive(false);
        StartCoroutine(destroyCoroutine(creepInList));
    }

    IEnumerator destroyCoroutine(GameObject obj)
    {
        float coroutineStartTime = Time.time;
        while (!obj.activeSelf)
        {
            if (Time.time - coroutineStartTime > inactivityTimeBeforeDestroy)
            {
                waiting.Remove(obj);
                GameObject.Destroy(obj);
                break;
            }
            yield return new WaitForSeconds(30);
        }
    }
}
