using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hexagon : InterractableTerrainElement
{

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

    public Rect spriteRect()
    {
        return GetComponent<SpriteRenderer>().sprite.rect;
    }

    void addNeighBourId(int id)
    {
        neighboursIds.Add(id);
    }

    public void calcId()
    {
        id = (int)(transform.position.x * 1000 + transform.position.z * 10);
    }

    public void setVisible(bool b)
    {
        renderer.enabled = b;
    }

    protected override void onMouseOver()
    {

    }

    protected override void onMouseExit()
    {

    }
}
