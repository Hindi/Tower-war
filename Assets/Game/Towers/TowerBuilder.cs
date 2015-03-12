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

    private BuySpawn lastBuilt;

    void Start()
    {
        lastBuilt = (BuySpawn)(-1);
    }

    public void build(BuySpawn spawn, Tile tile)
    {
        lastBuilt = spawn;
        int price = catalog.getPrefab(spawn).GetComponent<TowerMoney>().Price;
        if (canBuild(spawn, price))
        {
            purse.substract(price);
            tile.GetComponent<OccupentHolder>().addOccupent(factory.spawn(spawn, tile.transform.position));
        }
    }

    public void upgrade(Tile tile, TowerUpgrade towerUp)
    {
        tile.GetComponent<OccupentHolder>().addOccupent(towerUp.upgradeNow());
    }

    public bool canBuild(BuySpawn spawn, int price)
    {
        return (purse.canAfford(price));
    }

    public void buildLast(Tile tile)
    {
        if ((int)(lastBuilt) != -1)
            build(lastBuilt, tile);
    }
}
