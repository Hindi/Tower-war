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

    float width;
    float height;

    float heightBetweenLines;
    float widthBetweenColumn;

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

    public Vector3 spawnSquare(Vector3 position, bool odd)
    {
        //Calculate the dimension that we'll use
        Vector3 lastPosition = position;

        float offestW = width / 2;
        float offsetL = 0;
        if(odd)
            offsetL = width * 2 / 2.69f;

        for (int j = 0; j < 21; ++j)
        {
            int i;
            for (i = 0; i < 10; ++i)
            {
                lastPosition = new Vector3(position.x + offsetL + i * (width + offestW), position.y - j * heightBetweenLines, 0);
                instantiateTile(lastPosition);
            }

            //Update the offset to obtain a nice hexagonal tile
            if (offsetL == 0)
                offsetL = width * 2 / 2.69f;
            else
                offsetL = 0;
        }
        return new Vector3(position.x, lastPosition.y - heightBetweenLines);
    }

    public Vector3 spawnTriangle(Vector3 position, bool odd)
    {
        Vector3 lastPosition = position;

        int linewidth = 10;
        int tileForThisLine = 1;
        int currentLineId = 0;

        float offestW = width / 2;
        float offsetL = 0;
        if (odd)
            offsetL = width * 2 / 2.69f;

        while(tileForThisLine < linewidth)
        {
            int i;
            for (i = 0; i < linewidth; ++i)
            {
                if (i >= linewidth / 2 - (tileForThisLine +1) / 2 && i <= linewidth / 2 + tileForThisLine / 2)
                {
                    lastPosition = new Vector3(position.x + offsetL + i * (width + offestW), position.y - currentLineId * heightBetweenLines, 0);
                    instantiateTile(lastPosition);
                }
            }

            //Update the offset to obtain a nice hexagonal tile
            if (offsetL == 0)
            {
                offsetL = width * 2 / 2.69f;
            }
            else
                offsetL = 0;
            tileForThisLine++;
            currentLineId++;
        }
        return new Vector3(position.x, lastPosition.y - heightBetweenLines);
    }

    public void spawnTile(Vector3 position)
    {
        Rect rect = tileReference.GetComponent<SpriteSwitcher>().CurrentSprite.rect;
        width = rect.width / 100 * tileReference.transform.localScale.x;
        height = rect.height / 84 * tileReference.transform.localScale.y;
        heightBetweenLines = height / 2.4f;
        widthBetweenColumn = (width / 2.69f) + width;

        //Calculate the dimension that we'll use        
        StartTile = instantiateTile(new Vector3(position.x, position.y + 1, 0));
        int startId = StartTile.GetComponent<Tile>().Id;
        StartTile.GetComponent<SpriteSwitcher>().setIdleSprite();
        GetComponent<CreepSpawner>().StartTile = StartTile;

        position = spawnSquare(position, false);
        position = spawnTriangle(position, true);
        position = spawnSquare(position, false);

        EndTile = instantiateTile(new Vector3(position.x, position.y, 0));
        int endId = EndTile.GetComponent<Tile>().Id;
        GetComponent<CreepSpawner>().EndTile = EndTile;
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
