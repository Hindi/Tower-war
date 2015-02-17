using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hexagon : InterractableTerrainElement
{
    private Zone zone;
    public Zone Zone
    {
        get { return zone; }
        set 
        { 
            zone = value;
            transform.parent = value.transform;
        }
    }

    private int id;
    public int Id
    {
        get { return id; }
    }

    private List<int> neighboursIds;
    public List<int> NeighboursIds
    {
        get { return neighboursIds; }
    }

    private bool clicked = false;

    public Rect spriteRect()
    {
        return GetComponent<SpriteSwitcher>().CurrentSprite.rect;
    }

    void addNeighBourId(int id)
    {
        neighboursIds.Add(id);
    }

    public void calcId()
    {
        id = (int)(transform.position.x * 1000 + transform.position.y * 10);
    }

    public void setVisible(bool b)
    {
        renderer.enabled = b;
    }

    public override void onMouseOver()
    {
        if(!clicked)
            GetComponent<SpriteSwitcher>().setMouseOverSprite();
        zone.notifyMouseOver();
    }

    public override void onMouseExit()
    {
        resetToIdle();
        zone.notifyMouseExit();
    }

    public override void onMouseDown()
    {
        GetComponent<SpriteSwitcher>().setMouseClickSprite();
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
