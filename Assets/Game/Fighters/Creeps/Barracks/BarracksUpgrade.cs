using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BarracksUpgrade : MonoBehaviour {

    [System.Serializable]
    private class Upgrade
    {
        public Catalog catalog;

        public GameObject model;

        public int price;
    }

    private CreepSpawner creepSpawner;
    public CreepSpawner CreepSpawner
    {
        set { creepSpawner = value; }
    }

    [SerializeField]
    private GameObject currentModel;

    [SerializeField]
    private List<Upgrade> upgrades;


    private int currentVersionId;

	// Use this for initialization
	void Start () {
        currentVersionId = 0;
	}

    /*[RPC]
    public void upgradeRPC()
    {
        applyUpdate();
    }*/

    public void upgrade()
    {
        //TODO : Update UI buy infos !
        if(creepSpawner.upgrade(upgrades[currentVersionId + 1].price, upgrades[currentVersionId + 1].catalog))
        {
            applyUpdate();
            //photonView.RPC("upgradeRPC", PhotonTargets.OthersBuffered);
        }
    }

    private void applyUpdate()
    {
        currentVersionId++;
        currentModel.SetActive(false);
        currentModel = upgrades[currentVersionId].model;
        currentModel.SetActive(true);
    }
}
