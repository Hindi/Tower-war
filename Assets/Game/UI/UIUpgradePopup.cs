using UnityEngine;
using System.Collections;

public class UIUpgradePopup : UIElement
{
    [SerializeField]
    private Player player;

    private Tile currentTile;

    void Start()
    {
        setActive(false);
    }

    public void popUp(Tile tile)
    {
        popUp(tile.transform.position);
        currentTile = tile;
    }

    public void sell()
    {
        player.CmdRequestSell(currentTile.Id);
        hide();
    }

    public void upgrade()
    {
        player.CmdRequestUpgrade(currentTile.Id);
    }

    public void hide()
    {
        if (currentTile != null)
            currentTile.GetComponent<SpriteSwitcher>().setIdleSprite();
        setActive(false);
    }
}
