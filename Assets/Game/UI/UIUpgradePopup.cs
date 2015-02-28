﻿using UnityEngine;
using System.Collections;

public class UIUpgradePopup : UIElement
{
    private Tile currentTile;


    public void popUp(Tile tile)
    {
        popUp(tile.transform.position);
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
