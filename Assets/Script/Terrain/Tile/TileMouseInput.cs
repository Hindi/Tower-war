using UnityEngine;
using System.Collections;

public class TileMouseInput : InterractableTerrainElement
{

    [SerializeField]
    private OccupentHolder occupentHolder;

    private bool clicked = false;

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
        if (!clicked)
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
        GetComponent<SpriteSwitcher>().setMouseClickSprite();
        //TODO : Use UI to build and destroy the tower
        if (!occupentHolder.IsOccupied)
            occupentHolder.Occupent = GameObject.FindGameObjectWithTag("TowerBuilder").GetComponent<TowerBuilder>().buildTower(transform.position);
        else
        {
            GameObject.FindGameObjectWithTag("TowerBuilder").GetComponent<TowerBuilder>().destroyTower(occupentHolder.Occupent);
            occupentHolder.Occupent = null;
        }

        clicked = true;
    }

    public override void onMouseUp()
    {
        resetToIdle();
    }

    void resetToIdle()
    {
        GetComponent<SpriteSwitcher>().setIdleSprite();
        clicked = false;
    }

}
