using UnityEngine;
using System.Collections;

public class UIBuildPopup : UIElement {

    [SerializeField]
    private GameObject menu;

    private Tile currentTile;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void popUp(Tile tile)
    {
        setActive(true);
        Vector2 pos = Camera.main.WorldToScreenPoint(tile.transform.position);
        menu.transform.position = new Vector3(pos.x + 25, pos.y + 25);
        currentTile = tile;
    }

    public void build()
    {
        currentTile.GetComponent<OccupentHolder>().addOccupent(Factory.Instance.spawn(EnumSpawn.TOWER, currentTile.transform.position));
        hide();
    }

    public void hide()
    {
        currentTile.GetComponent<SpriteSwitcher>().setIdleSprite();
        setActive(false);
    }
}
