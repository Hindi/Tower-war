using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Tile))]
public class TileMouseInput : InterractableTerrainElement
{
    [SerializeField]
    private OccupentHolder occupentHolder;

    Tile tile;
    Player player;

    void Start()
    {
        tile = GetComponent<Tile>();
    }

    public void setPlayer(Player p)
    {
        player = p;
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
                    tile.Zone.showUpgradePopup(tile);
                }
                else if (occupentHolder.canBuild())
                {
                    GetComponent<SpriteSwitcher>().setSelected();
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

    protected override bool canInterract()
    {
        if (player == null)
            return false;
        return base.canInterract() && player.isMine();
    }
}
