using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Pathfinder))]
public class CreepSpawner : MonoBehaviour {

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
    private GOF.GOFactory factory;
    public GOF.GOFactory Factory
    { set { factory = value; } }

    [SerializeField]
    private Purse purse;

    [SerializeField]
    private Catalog catalog;

    [SerializeField]
    private Income income;

    private UIBuyCreepsPopup buyPopup;
    public UIBuyCreepsPopup BuyPopup
    {
        set 
        { 
            buyPopup = value;
            buyPopup.upgrade(catalog);
        }
    }

    PhotonView photonView;

	// Use this for initialization
	void Start () {
        pathfinder = GetComponent<Pathfinder>();
        photonView = GetComponent<PhotonView>();

        EventManager.AddListener(EnumEvent.START, onGameStart);
	}

    [RPC]
    public void spawnRPC(int index)
    {
        spawn(index);
    }

    private void spawn(int index)
    {
        string name = catalog.getPrefab(index).name;
        GameObject creep = factory.spawn(name, startTile.transform.position);
        creep.GetComponent<CreepMovement>().Path = pathfinder.Result;
        creep.GetComponent<CreepMovement>().Pathfinder = pathfinder;
    }

    public void requestSpawn(int index)
    {
        if (catalog.contains(index))
        {
            int price = catalog.getPrefab(index).GetComponent<CreepMoney>().Price;

            if (purse.canAfford(price))
            {
                purse.substract(price);
                income.increaseIncome(catalog.getPrefab(index).GetComponent<CreepMoney>().IncomeIncrease);
                if (PhotonNetwork.offlineMode)
                    spawn(index);
                else
                    photonView.RPC("spawnRPC", PhotonTargets.Others, index);
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
