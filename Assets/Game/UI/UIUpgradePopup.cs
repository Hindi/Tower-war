using UnityEngine;
using System.Collections;

public class UIUpgradePopup : UIElement
{
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
        menu.transform.position = new Vector3(pos.x, pos.y);
        currentTile = tile;
    }

    public void sell()
    {
        if(currentTile != null)
            currentTile.GetComponent<OccupentHolder>().destroyOccupent();
        hide();
    }

    public void hide()
    {
        if (currentTile != null)
            currentTile.GetComponent<SpriteSwitcher>().setIdleSprite();
        setActive(false);
    }
}
