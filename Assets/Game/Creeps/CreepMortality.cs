﻿using UnityEngine;
using System.Collections;

public class CreepMortality : MonoBehaviour {

    [SerializeField]
    private int maxHp;

    [SerializeField]
    private UIHealthBar healthBar;

    private int currentHp;

    void Start()
    {
        reset();
        healthBar = (PhotonNetwork.Instantiate("HealthBar", transform.position, Quaternion.identity, 0)).GetComponent<UIHealthBar>();
        healthBar.init(gameObject);
    }

    public void reset()
    {
        currentHp = maxHp;
        healthBar.reset();
    }

    public bool takeDamage(int dmg)
    {
        currentHp = Mathf.Max(0, currentHp - dmg);
        healthBar.setHealthPercentage((float)currentHp / (float)maxHp);
        if (currentHp == 0)
        {
            die();
            return true;
        }
        return false;
    }

    public void kill()
    {
        takeDamage(currentHp);
    }

    private void die()
    {
        FxSpawner.Instance.spawn(0, transform.position);
        GetComponent<CreepActivity>().Active = false;
    }
}
