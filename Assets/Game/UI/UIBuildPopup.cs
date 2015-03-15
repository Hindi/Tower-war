using UnityEngine;
using System.Collections;

public class UIBuildPopup : UIElement {

    [SerializeField]
    private TowerBuilder towerBuilder;

    private Tile currentTile;

    public void popUp(Tile tile)
    {
        popUp(tile.transform.position);
        currentTile = tile;
    }

    public void build(int i)
    {
        if (currentTile != null)
            towerBuilder.build(i, currentTile);

        hide();
    }

    public void hide()
    {
        if (currentTile != null)
            currentTile.GetComponent<SpriteSwitcher>().setIdleSprite();
        setActive(false);
    }
}
