﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Tile))]
public class TileMouseInput : InterractableTerrainElement
{
    [SerializeField]
    private OccupentHolder occupentHolder;

    Tile tile;
    [SerializeField]
    Player player;

    void Start()
    {
        tile = GetComponent<Tile>();
        player = tile.Player;
    }

    public Rect spriteRect()
    {
        return GetComponent<SpriteSwitcher>().CurrentSprite.rect;
    }

    public void setVisible(bool b)
    {
        GetComponent<Renderer>().enabled = b;
    }

    public override void onMouseOver()
    {
        if (canInterract())
        {
            GetComponent<SpriteSwitcher>().setMouseOverSprite();
        }
    }

    public override void onMouseExit()
    {
        if (canInterract())
        {
            resetToIdle();
        }
    }

    public void buildLast()
    {
        TowerBuilder.Instance.buildLast(tile);
    }

    public override void onMouseDown()
    {
        if (canInterract())
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                player.CmdRequestBuild(tile.Id, TowerBuilder.Instance.SelectedTower);
            }
            else
            {
                if (occupentHolder.IsOccupied)
                {
                    GetComponent<SpriteSwitcher>().setSelected();
                    UI.Instance.showUpgradePopupp(tile);
                }
                else if (occupentHolder.canBuild())
                {
                    GetComponent<SpriteSwitcher>().setSelected();
                    UI.Instance.showBuildPopup(tile);
                }
            }
        }
    }

    public override void onMouseUp()
    {
        if (canInterract())
        {
            resetToIdle();
        }
    }

    void resetToIdle()
    {
        GetComponent<SpriteSwitcher>().setPreviousSprite();
    }
}
