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

    private BuySpawn lastBuilt;

    void Start()
    {
        lastBuilt = (BuySpawn)(-1);
    }

    public void build(BuySpawn spawn, Tile tile)
    {
        lastBuilt = spawn;
        if (canBuild(spawn))
            tile.GetComponent<OccupentHolder>().addOccupent(factory.spawn(spawn, tile.transform.position));
    }

    public void upgrade(Tile tile, TowerUpgrade towerUp)
    {
        tile.GetComponent<OccupentHolder>().addOccupent(towerUp.upgradeNow());
    }

    public bool canBuild(BuySpawn spawn)
    {
        return true;
    }

    public void buildLast(Tile tile)
    {
        if ((int)(lastBuilt) != -1)
            build(lastBuilt, tile);
    }
}
