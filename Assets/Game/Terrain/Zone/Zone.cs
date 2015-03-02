using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Zone : MonoBehaviour {

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

	// Use this for initialization
	void Awake () {
        tileDict = new Dictionary<int, Tile>();
	}

    public void spawnTile(Vector3 position)
    {
        //Calculate the dimension that we'll use
        float width = 0;
        float height = 0;
        float offestW;
        float offsetL = 0;

        GameObject currentTile = null;
        Tile currTileScript;
        for (int j = 0; j < 20; ++j)
        {
            for (int i = 0; i < 10; ++i)
            {
                Rect rect = tileReference.GetComponent<SpriteSwitcher>().CurrentSprite.rect;
                width = rect.width / 100;
                height = rect.height / 84;
                offestW = width / 2;
                currentTile = PhotonNetwork.Instantiate("Tile", new Vector3(position.x + offsetL + i * (width + offestW), position.y + j * height / 2.4f, 0), Quaternion.identity, 0);
                currTileScript = currentTile.GetComponent<Tile>();

                currTileScript.Zone = this;
                currTileScript.calcId();

                tileDict.Add(currTileScript.Id, currTileScript);
                if (i == 0 && j == 0)
                {
                    GetComponent<CreepSpawner>().StartTile = currentTile;
                    StartTile = currentTile;
                }
            }
            if (offsetL == 0)
                offsetL = width * 2 / 2.69f;
            else
                offsetL = 0;
        }
        GetComponent<CreepSpawner>().EndTile = currentTile;
        EndTile = currentTile;

        StartCoroutine(catchNeighbourCoroutine());
        PhotonNetwork.Instantiate("Barracks", new Vector3(position.x - 1, position.y, 0), Quaternion.identity, 0);
    }

    IEnumerator catchNeighbourCoroutine()
    {
        yield return null;
        foreach (KeyValuePair<int, Tile> p in tileDict)
            p.Value.catchNeighboursIds();
        EventManager.Raise(EnumEvent.START);
    }
	
	// Update is called once per frame
	void Update () {
	
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
