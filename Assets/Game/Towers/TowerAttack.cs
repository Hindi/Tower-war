using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerAttack : MonoBehaviour {

    [SerializeField]
    private int damage;

    [SerializeField]
    private int radius;

    [SerializeField]
    private float fireCooldown;
    private float lastShotTime;

    List<GameObject> targets;

    private GameObject currentTarget;
    private GameObject endTile;

	// Use this for initialization
	void Start () {
        lastShotTime = Time.time;
        targets = new List<GameObject>();
        GetComponent<SphereCollider>().radius = radius;
	}
	
	// Update is called once per frame
	void Update () {
        if (currentTarget != null)
        {
            if(currentTarget.activeSelf == false)
            {
                currentTarget = null;
                return;
            }
            if (canFire())
                fire();
        }
	}

    bool canFire()
    {
        return (Time.time - lastShotTime > fireCooldown);
    }

    void fire()
    {
        var heading = currentTarget.transform.position - transform.position;
        float dist = heading.magnitude;
        Debug.DrawRay(transform.position, heading / dist);
        if (currentTarget.GetComponent<CreepMortality>().takeDamage(damage))
            excludeTarget(currentTarget);
        lastShotTime = Time.time;
    }

    void pickTarget()
    {
        if(targets.Count > 0)
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
        }
        pickTarget();
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Creep"))
        {
            excludeTarget(col.gameObject);
        }
        pickTarget();
    }

    void excludeTarget(GameObject obj)
    {
        if (targets.Exists(t => t.GetComponent<FactoryModel>().Id == obj.GetComponent<FactoryModel>().Id))
        {
            targets.Remove(obj);
            if (currentTarget != null)
                if (currentTarget.GetComponent<FactoryModel>().Id == obj.GetComponent<FactoryModel>().Id)
                    currentTarget = null;
        }
    }
}
