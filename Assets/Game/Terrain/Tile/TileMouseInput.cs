using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Tile))]
public class TileMouseInput : InterractableTerrainElement
{

    [SerializeField]
    private OccupentHolder occupentHolder;

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
        GetComponent<SpriteSwitcher>().setMouseOverSprite();
        GetComponent<Tile>().Zone.notifyMouseOver();
    }

    public override void onMouseExit()
    {
        resetToIdle();
        GetComponent<Tile>().Zone.notifyMouseExit();
    }

    public override void onMouseDown()
    {
        //TODO : Use UI to build and destroy the tower
        if (occupentHolder.IsOccupied)
        {
            occupentHolder.destroyOccupent();
        }
        else if(occupentHolder.canBuild())
        {
            occupentHolder.addOccupent(Factory.Instance.spawn(EnumSpawn.TOWER, transform.position));
        }
    }

    public override void onMouseUp()
    {
        resetToIdle();
    }

    void resetToIdle()
    {
        GetComponent<SpriteSwitcher>().setPreviousSprite();
    }

}
