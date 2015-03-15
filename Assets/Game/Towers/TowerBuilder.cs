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

    private int lastBuilt;

    void Start()
    {
        lastBuilt = -1;
    }

    public void build(int index, Tile tile)
    {
        lastBuilt = index;
        int price = catalog.getPrefab(index).GetComponent<TowerMoney>().Price;
        if (canBuild(price))
        {
            purse.substract(price);
            tile.GetComponent<OccupentHolder>().addOccupent(factory.spawn(index, tile.transform.position));
        }
    }

    public void upgrade(Tile tile, TowerUpgrade towerUp)
    {
        int price = towerUp.GetComponent<TowerMoney>().UpgradePrice;
        if(canBuild(price) && towerUp.hasAnUpgrade())
        {
            tile.GetComponent<OccupentHolder>().destroyOccupent();
            tile.GetComponent<OccupentHolder>().addOccupent(towerUp.upgradeNow());
            purse.substract(price);
        }
    }

    public bool canBuild(int price)
    {
        return (purse.canAfford(price));
    }

    public void buildLast(Tile tile)
    {
        if ((int)(lastBuilt) != -1)
            build(lastBuilt, tile);
    }
}
