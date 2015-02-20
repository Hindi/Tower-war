using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Zone : MonoBehaviour {

    [SerializeField]
    private GameObject tilePrefab;

    private GameObject startTile;
    public GameObject StartTile
    {
        get { return startTile; }
    }

    private GameObject endTile;
    public GameObject EndTile
    {
        get { return endTile; }
    }

    private Dictionary<int, Tile> tileDict;
    public Dictionary<int, Tile> TileDict
    {
        get { return tileDict; }
    }

	// Use this for initialization
	void Start () {
        tileDict = new Dictionary<int, Tile>();

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
                currentTile = (GameObject)Instantiate(tilePrefab);
                currTileScript = currentTile.GetComponent<Tile>();
                Rect rect = currentTile.GetComponent<SpriteSwitcher>().CurrentSprite.rect;

                width = rect.width / 100;
                height = rect.height / 84;
                offestW = width / 2;
                currentTile.transform.position = new Vector3(offsetL + i * (width + offestW), j * height / 2.4f, 0);
                currTileScript.Zone = this;
                currTileScript.calcId();

                tileDict.Add(currTileScript.Id, currTileScript);
                if (i == 0 && j == 0)
                    startTile = currentTile;
            }
            if (offsetL == 0)
                offsetL = width * 2 / 2.69f;
            else
                offsetL = 0;
        }
        endTile = currentTile;

        foreach (KeyValuePair<int, Tile> p in tileDict)
            p.Value.catchNeighboursIds();

        GetComponent<Pathfinder>().findPath();
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
}
