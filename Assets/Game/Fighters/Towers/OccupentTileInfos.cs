using UnityEngine;
using System.Collections;

public class OccupentTileInfos : MonoBehaviour
{
    private Tile tile;
    public Tile Tile
    {
        get { return tile; }
        set { tile = value; }
    }

    private Zone zone;
    public Zone Zone
    {
        get { return zone; }
        set { zone = value; }
    }
}
