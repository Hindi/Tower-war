using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class TowerFocus : NetworkBehaviour {

    private int radius;
    public int Radius
    {
        get { return radius; }
        set { radius = value; }
    }

    List<GameObject> targets;

    [SerializeField]
    private GameObject currentTarget;
    public GameObject CurrentTarget
    {
        get { return currentTarget; }
    }

    private GameObject endTile;
    private Vector3 idleDirection;
    private Vector3 lookAtPosition;

    // Use this for initialization
    void Start()
    {

        targets = new List<GameObject>();
        GetComponent<SphereCollider>().radius = radius;

        if (isServer)
            idleDirection = GetComponent<OccupentTileInfos>().Zone.StartTile.transform.position;
        idleDirection.z -= 0.1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (isServer)
        {
            if (currentTarget != null)
            {
                lookAtPosition = currentTarget.transform.position;
                if (currentTarget.activeSelf == false)
                {
                    currentTarget = null;
                }
            }
            else
            {
                lookAtPosition = idleDirection;
                pickTarget();
            }
            GetComponent<TowerHead>().lookAt(lookAtPosition);
        }
        else
        {
            GetComponent<TowerHead>().networkLookAt();
        }
    }

    void pickTarget()
    {
        for (int i = 0; i < targets.Count; )
        {
            if (!targets[i].activeSelf)
                targets.Remove(targets[i]);
            else
                i++;
        }
        if (targets.Count > 0)
        {
            float min = 999999;
            int minId = 0;

            for (int i = 0; i < targets.Count; ++i)
            {
                float distance = Vector3.Distance(targets[i].transform.position, GetComponent<OccupentTileInfos>().Zone.EndTile.transform.position);
                if (distance < min)
                {
                    min = distance;
                    minId = i;
                }
            }
            currentTarget = targets[minId];
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius * transform.localScale.x);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Creep"))
        {
            targets.Add(col.gameObject);
            col.GetComponent<CreepTargetKeeper>().notifyTarget(this);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Creep"))
        {
            excludeTarget(col.gameObject);
        }
    }

    public void excludeTarget(GameObject obj)
    {
        if (targets.Contains(obj))
        {
            targets.Remove(obj);
            if (currentTarget != null)
                if (currentTarget.GetComponent<FactoryModel>().Id == obj.GetComponent<FactoryModel>().Id)
                    currentTarget = null;
        }
    }
}
