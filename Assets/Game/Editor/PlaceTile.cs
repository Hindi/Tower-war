using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(Tile))]
public class PlaceTile : Editor
{
    Tile tile;

    public void OnEnable()
    {
        tile = (Tile)target;
        tile.calcId();
    }

    void OnSceneGUI()
    {
        if (Selection.activeObject)
        {
            Event e = Event.current;
            if (e.isKey && e.character == 'a')
            {
                tile.GetComponent<Tile>().catchNeighboursIds();
            }
        }
    }
}
