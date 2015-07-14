using UnityEngine;
using System.Collections;

public class TowerBuilder : MonoBehaviour {

    private static TowerBuilder instance;

    public static TowerBuilder Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<TowerBuilder>();
            return instance;
        }
    }

    [SerializeField]
    Factory factory;

    [SerializeField]
    private Purse purse;

    [SerializeField]
    private Catalog catalog;

    [SerializeField]
    private UIBuildPopup UIBuild;

    private int selectedTower;
    public int SelectedTower
    {
        set { selectedTower = value; }
        get { return selectedTower; }
    }

    void Start()
    {
        selectedTower = -1;
        UIBuild.upgrade(catalog);
    }

    public bool build(int index, Tile tile)
    {
        selectedTower = index;
        int price = catalog.getPrefab(index).GetComponent<TowerStats>().Price;

        if (canBuild(price))
        {
            purse.substract(price);
            tile.GetComponent<OccupentHolder>().addOccupent(factory.spawn(index, tile.transform.position));
            return true;
        }
        return false;
    }

    public void sell(Tile tile)
    {
        OccupentHolder oh = tile.GetComponent<OccupentHolder>();
        purse.add(oh.occupent.GetComponent<TowerMoney>().Price);
        oh.destroyOccupent();
    }

    public void upgrade(Tile tile, TowerUpgrade towerUp)
    {
        int price = towerUp.GetComponent<TowerMoney>().UpgradePrice;
        if(canBuild(price) && towerUp.hasAnUpgrade())
        {
            tile.GetComponent<OccupentHolder>().destroyOccupent();
            tile.GetComponent<OccupentHolder>().addOccupent(towerUp.upgradeNow(tile.transform.position, factory));
            purse.substract(price);
        }
    }

    public bool canBuild(int price)
    {
        return (purse.canAfford(price));
    }

    public void buildLast(Tile tile)
    {
        if ((int)(selectedTower) != -1)
            build(selectedTower, tile);
    }
}
