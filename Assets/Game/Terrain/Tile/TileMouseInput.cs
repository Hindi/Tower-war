using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Tile))]
public class TileMouseInput : InterractableTerrainElement
{

    [SerializeField]
    private OccupentHolder occupentHolder;

    Tile tile;

    void Start()
    {
        tile = GetComponent<Tile>();
    }

    public Rect spriteRect()
    {
        return GetComponent<SpriteSwitcher>().CurrentSprite.rect;
    }

    public void setVisible(bool b)
    {
        renderer.enabled = b;
    }

    public override void onMouseOver()
    {
        if (canInterract())
        {
            GetComponent<SpriteSwitcher>().setMouseOverSprite();
            tile.Zone.notifyMouseOver();
        }
    }

    public override void onMouseExit()
    {
        if (canInterract())
        {
            resetToIdle();
            tile.Zone.notifyMouseExit();
        }
    }

    public override void onMouseDown()
    {
        if (canInterract())
        {
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                if (occupentHolder.canBuild())
                {
                    TowerBuilder.Instance.buildLast(tile);
                }
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
