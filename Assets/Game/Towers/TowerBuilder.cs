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

    private EnumSpawn lastBuilt;

    void Start()
    {
        lastBuilt = (EnumSpawn)(-1);
    }

    public void build(EnumSpawn spawn, Tile tile)
    {
        lastBuilt = spawn;
        if (canBuild())
            tile.GetComponent<OccupentHolder>().addOccupent(factory.spawn(spawn, tile.transform.position));
    }

    public bool canBuild()
    {
        return true;
    }

    public void buildLast(Tile tile)
    {
        if ((int)(lastBuilt) != -1 && canBuild())
            tile.GetComponent<OccupentHolder>().addOccupent(factory.spawn(lastBuilt, tile.transform.position));
    }
}
