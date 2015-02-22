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
        if (!occupentHolder.hasCreepOnIt())
        {
            if (!occupentHolder.IsOccupied && GetComponent<Tile>().Zone.canBuildHere(GetComponent<Tile>()))
                occupentHolder.addOccupent(Factory.Instance.spawn(EnumSpawn.TOWER, transform.position));
            else
            {
                //GameObject.FindGameObjectWithTag("TowerBuilder").GetComponent<TowerBuilder>().destroyTower(occupentHolder.Occupent);
                occupentHolder.destroyOccupent();
            }
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
