using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tile : MonoBehaviour
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

    private int g;
    public int G
    {
        get { return g; }
        set { g = value; }
    }

    private int h;
    public int H
    {
        get { return h; }
        set { h = value; }
    }

    public int F
    {
        get { return (H + G); }
    }

    [SerializeField]
    private int id;
    public int Id
    {
        get { return id; }
    }

    private int parentId;
    public int ParentId
    {
        get { return parentId; }
        set { parentId = value; }
    }

    private bool walkable;
    public bool Walkable
    {
        get { return walkable; }
        set { walkable = value; }
    }

    private List<int> neighboursIds;
    public List<int> NeighboursIds
    {
        get { return neighboursIds; }
    }
    
    public void manHattanDistance(Tile endTile)
    {
        int x = Mathf.Abs((int)(transform.position.x - endTile.transform.position.x));
        int y = Mathf.Abs((int)(transform.position.y - endTile.transform.position.y));
	    h = x + y;
    }

    void addNeighBourId(int id)
    {
        neighboursIds.Add(id);
    }

    public void calcId()
    {
        id = (int)(transform.position.x * 1000 + transform.position.y * 10);
    }

    
    public void catchNeighboursIds()
    {
        neighboursIds = new List<int>();
        rayCast(Vector2.up);
        rayCast(-Vector2.up);
        rayCast(Vector2.right);
        rayCast(-Vector2.right);
        rayCast(Vector2.up + Vector2.right);
        rayCast(Vector2.up - Vector2.right);
        rayCast(-Vector2.up + Vector2.right);
        rayCast(-Vector2.up - Vector2.right);
    }

    void enableTileCollider(bool b)
    {
        GetComponent<SpriteSwitcher>().SpriteHolder.GetComponent<PolygonCollider2D>().enabled = b;
    }

    void rayCast(Vector2 direction)
    {
        enableTileCollider(false);
        RaycastHit2D hitUp = Physics2D.Raycast(transform.position, direction, 1);
        enableTileCollider(true);

        if (hitUp.transform != null)// && hitUp.collider.tag == "Tile")
        {
            int id = hitUp.collider.gameObject.GetComponent<Tile>().Id;
            if (!neighboursIds.Contains(id))
            {
                neighboursIds.Add(id);
            }
        }
    }
}

