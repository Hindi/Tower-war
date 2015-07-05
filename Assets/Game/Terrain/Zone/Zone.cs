using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class Zone : NetworkBehaviour {

    [SerializeField]
    private GameObject tileReference;

    private Dictionary<int, Tile> tileDict;
    public Dictionary<int, Tile> TileDict
    {
        get { return tileDict; }
    }

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
    private Player player;

    [SerializeField]
    private int lineCount;

    [SerializeField]
    private int columnCount;

    [SerializeField]
    private GameObject startTilePrefab;
    [SerializeField]
    private GameObject endTilePrefab;
    [SerializeField]
    private GameObject tilePrefab;
    [SerializeField]
    private Income income;
    [SerializeField]
    private UIUpgradePopup upgradePopup;

	// Use this for initialization
	void Awake () {
        tileDict = new Dictionary<int, Tile>();
	}

    void OnDisable()
    {

    }

    private void Start()
    {
        if (isServer)
        {
            spawnTile(transform.position);
        }
        EventManager.AddListener(EnumEvent.START, onGameStart);
    }

    public void onGameStart()
    {
        if (isServer)
            income.startCounting();
    }

    public void showUpgradePopup(Tile tile)
    {
        upgradePopup.popUp(tile);
    }

    public void spawnTile(Vector3 position)
    {
        //Calculate the dimension that we'll use
        Rect rect = tileReference.GetComponent<SpriteSwitcher>().CurrentSprite.rect;
        float offsetL = 0;
        float width = rect.width / 100 * tileReference.transform.localScale.x;
        float height = rect.height / 84 * tileReference.transform.localScale.y;

        float heightBetweenLines = height / 2.4f;
        float widthBetweenColumn = (width / 2.69f) + width;
        float offestW = width / 2;

        StartTile = instantiateTile(new Vector3(position.x + widthBetweenColumn * columnCount / 2, position.y + heightBetweenLines * lineCount + 1, 0));
        EndTile = instantiateTile(new Vector3(position.x + widthBetweenColumn * columnCount / 2, position.y - 1, 0));
        int startId = StartTile.GetComponent<Tile>().Id;
        int endId = EndTile.GetComponent<Tile>().Id;

        GetComponent<CreepSpawner>().EndTile = EndTile;
        GetComponent<CreepSpawner>().StartTile = StartTile;
        GameObject lastInstantiatedTile = null;

        for (int j = 0; j < lineCount; ++j)
        {
            int i;
            for (i = 0; i < columnCount; ++i)
            {
                lastInstantiatedTile = instantiateTile(new Vector3(position.x + offsetL + i * (width + offestW), position.y + j * heightBetweenLines, 0));

                //Add the neighbours ids to the start and end tile
                if (j <= 2)
                    addNeighbours(endId, lastInstantiatedTile.GetComponent<Tile>().Id);
                else if (j >= lineCount - 2)
                    addNeighbours(startId, lastInstantiatedTile.GetComponent<Tile>().Id);
            }

            //Update the offset to obtain a nice hexagonal tile
            if (offsetL == 0)
                offsetL = width * 2 / 2.69f;
            else
                offsetL = 0;
        }

        StartCoroutine(catchNeighbourCoroutine());
    }

    private void addNeighbours(int id1, int id2)
    {
        tileDict[id1].addNeighBourId(id2);
        tileDict[id2].addNeighBourId(id1);
    }

    GameObject instantiateTile(Vector3 position)
    {
        GameObject currentTile = (GameObject)Instantiate(tilePrefab, position, Quaternion.identity);
        NetworkServer.Spawn(currentTile);

        Tile currTileScript;
        currTileScript = currentTile.GetComponent<Tile>();
        currTileScript.Zone = this;
        currTileScript.Player = player;
        currTileScript.calcId();

        tileDict.Add(currTileScript.Id, currTileScript);
        return currentTile;
    }

    IEnumerator catchNeighbourCoroutine()
    {
        yield return null;
        foreach (KeyValuePair<int, Tile> p in tileDict)
            p.Value.catchNeighboursIds();
        EventManager.Raise(EnumEvent.START);
    }

    public bool canBuildHere(Tile tile)
    {
        return GetComponent<Pathfinder>().canAddObstacle(tile);
    }

    public bool canBuildHere(int id)
    {
        if (tileDict.ContainsKey(id))
        {
            Tile tile = tileDict[id];
            OccupentHolder oh = tile.GetComponent<OccupentHolder>();

            return oh.canBuild();
        }
        else
            return false;
    }

    void OnDestroy()
    {
        EventManager.RemoveListener(EnumEvent.START, onGameStart);
    }
}
