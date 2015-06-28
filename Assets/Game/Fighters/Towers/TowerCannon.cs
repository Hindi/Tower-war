﻿using UnityEngine;
using System.Collections;

public class TowerCannon : MonoBehaviour
{
    RaycastHit hit;

    [SerializeField]
    private float fireCooldown;
    private float lastShotTime;

    [SerializeField]
    private TowerFocus towerFocus;

    [SerializeField]
    private MoveTowardAndHide projectile;
    [SerializeField]
    private int damage;

    void Start()
    {
        lastShotTime = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
        //if (photonView.isMine)
            if (towerFocus.CurrentTarget != null )
                rayCast();
    }

    bool canFire()
    {
        return (Time.time - lastShotTime > fireCooldown);
    }

    void fire(GameObject target)
    {
        var heading = towerFocus.CurrentTarget.transform.position - transform.position;
        float dist = heading.magnitude;
        projectile.move(transform.position, target.transform.position);
        target.GetComponent<CreepMortality>().takeDamage(damage);
        lastShotTime = Time.time;
    }

    void rayCast()
    {
        if (Physics.Raycast(transform.position, transform.forward * towerFocus.Radius * towerFocus.transform.localScale.x, out hit))
        {
            if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Creep"))
            {
                if (canFire())
                    fire(hit.collider.gameObject);
            }
        }
    }
}