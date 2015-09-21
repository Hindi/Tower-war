using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class TowerCannon : NetworkBehaviour
{
    RaycastHit hit;

    private float fireCooldown;
    public float FireCooldown
    {
        set { fireCooldown = value; }
    }

    private float lastShotTime;

    private TowerFocus towerFocus;
    private TowerHead towerHead;
    private TowerStats towerStat;

    [SerializeField]
    private Transform headObject;

    void Start()
    {
        lastShotTime = Time.time;
        towerFocus = GetComponent<TowerFocus>();
        towerHead = GetComponent<TowerHead>();
        towerStat = GetComponent<TowerStats>();
    }
	
	public void tryShot()
    {
        if (towerFocus.CurrentTarget != null)
            rayCast();
    }

    bool canFire()
    {
        return (Time.time - lastShotTime > fireCooldown);
    }

    void fire(GameObject target)
    {
        towerHead.moveProjectile(headObject.position, target.transform.position);
        CombatManager.solveAttack(towerStat, target.GetComponent<CreepStats>());
        lastShotTime = Time.time;
    }

    void rayCast()
    {
        if (canFire())
        {
            if (Physics.Raycast(headObject.position, headObject.forward * towerFocus.Radius * towerFocus.transform.localScale.x, out hit))
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Creep"))
                {
                    fire(hit.collider.gameObject);
                }
            }
        }
    }
}
