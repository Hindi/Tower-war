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
    private int lineCount;

    [SerializeField]
    private int columnCount;

    [SerializeField]
    private UIBuyCreepsPopup buyPopup;

    [SerializeField]
    private GameObject startTilePrefab;
    [SerializeField]
    private GameObject endTilePrefab;
    [SerializeField]
    private GameObject tilePrefab;

	// Use this for initialization
	void Awake () {
        tileDict = new Dictionary<int, Tile>();
        EventManager.AddListener(EnumEvent.START, onGameStart);
	}

    void OnDisable()
    {

    }

    public void onGameStart()
    {
        GetComponent<CreepSpawner>().BuyPopup = buyPopup;
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
        Debug.Log("pouet"); 
        StartTile = instantiateTile("StartTile", new Vector3(position.x  + widthBetweenColumn * columnCount / 2, position.y + heightBetweenLines * lineCount +1, 0));
        EndTile = instantiateTile("EndTile", new Vector3(position.x + widthBetweenColumn * columnCount / 2, position.y - 1, 0));
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
                lastInstantiatedTile = instantiateTile("Tile", new Vector3(position.x + offsetL + i * (width + offestW), position.y + j * heightBetweenLines, 0));

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

        //Create andi nit barrack
        /*var obj  = PhotonNetwork.Instantiate("Barracks", new Vector3(position.x - 1, position.y, 0), Quaternion.identity, 0);
        obj.GetComponent<BarracksUpgrade>().CreepSpawner = GetComponent<CreepSpawner>();
        buyPopup.BarackUpgrade = obj.GetComponent<BarracksUpgrade>();*/
    }

    private void addNeighbours(int id1, int id2)
    {
        tileDict[id1].addNeighBourId(id2);
        tileDict[id2].addNeighBourId(id1);
    }

    GameObject instantiateTile(string tileType, Vector3 position)
    {
        GameObject currentTile = (GameObject)Instantiate(tilePrefab, position, Quaternion.identity);
        NetworkServer.Spawn(currentTile);

        Tile currTileScript;
        currTileScript = currentTile.GetComponent<Tile>();
        currTileScript.Zone = this;
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

    public void notifyMouseOver()
    {
        foreach (Transform g in transform)
        {
            g.GetComponent<Fader>().setRendererVisible(true);
        }
    }

    public void notifyMouseExit()
    {
        foreach(Transform g in transform)
        {
            g.GetComponent<Fader>().setRendererVisible(false);
        }
    }

    public bool canBuildHere(Tile tile)
    {
        return GetComponent<Pathfinder>().canAddObstacle(tile);
    }
}
