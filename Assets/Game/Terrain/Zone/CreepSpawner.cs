using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

[RequireComponent(typeof(Pathfinder))]
public class CreepSpawner : NetworkBehaviour {

    private Pathfinder pathfinder;

    private GameObject startTile;
    public GameObject StartTile
    {
        get { return startTile; }
        set { startTile = value; }
    }

    private GameObject endTile;
    public GameObject EndTile
    {
        get { return endTile; }
        set { endTile = value; }
    }

    [SerializeField]
    private Factory factory;
    public Factory Factory
    { set { factory = value; } }

    [SerializeField]
    private Purse purse;

    [SerializeField]
    private Catalog catalog;

    [SerializeField]
    private Income income;

    [SerializeField]
    private UIBuyCreepsPopup buyPopup;

    private Zone zone;

	// Use this for initialization
	void Start () {
        zone = GetComponent<Zone>();
        pathfinder = GetComponent<Pathfinder>();
        buyPopup.init(this);
        buyPopup.upgrade(catalog);
        EventManager.AddListener(EnumEvent.START, onGameStart);
	}

    private void spawn(int index)
    {
        GameObject creep = factory.spawn(index, startTile.transform.position);
        CreepMovement cm = creep.GetComponent<CreepMovement>();
        cm.Path = pathfinder.Result;
        cm.Pathfinder = pathfinder;
        cm.CurrentZone = zone;
    }

    public void requestSpawn(int index)
    {
        CmdRequestSpawn(index);
    }

    [Command]
    public void CmdRequestSpawn(int index)
    {
        if (catalog.contains(index))
        {
            int price = catalog.getPrefab(index).GetComponent<CreepMoney>().Price;

            if (purse.canAfford(price))
            {
                purse.substract(price);
                income.increaseIncome(catalog.getPrefab(index).GetComponent<CreepMoney>().IncomeIncrease);
                if (TWNetworkManager.DEBUG)
                    spawn(index);
                else 
                    Debug.LogWarning("Not implemented in non debug mode");
            }
        }
    }

    void onGameStart()
    {
        updatePath();
    }

    void updatePath()
    {
        pathfinder.StartId = startTile.GetComponent<Tile>().Id;
        pathfinder.GoalId = endTile.GetComponent<Tile>().Id;
        pathfinder.findPath();
    }

    public bool upgrade(int price, Catalog newCatalog)
    {
        if(purse.canAfford(price))
        {
            purse.substract(price);
            catalog = newCatalog;
            buyPopup.upgrade(catalog);
            return true;
        }
        return false;
    }
}
