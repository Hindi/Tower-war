using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class Pathfinder : MonoBehaviour {

    [SerializeField]
    private Zone zone;

    private Dictionary<int, Tile> tileDict;
    private List<int> openList;
    private List<int> closedList;
    private List<int> result;

    private int startId;
    private int goalId;
    private int currentId;

    void Start()
    {
        result = new List<int>();
        closedList = new List<int>();
        openList = new List<int>();

        EventManager.AddListener(EnumEvent.TILEMAPUPDATE, onMapUpdate);
    }

    public void onMapUpdate()
    {
        findPath();
    }

    public void findPath()
    {

        startId = zone.StartTile.GetComponent<Tile>().Id;
        goalId = zone.EndTile.GetComponent<Tile>().Id;

        //Don't look for a path if goal = start
        if (startId == goalId)
        {
            result.Add(startId);
            return;
        }

        tileDict = zone.TileDict;
        openList.Clear();
        closedList.Clear();
        result.Clear();

        currentId = startId;
        closedList.Add(startId);
        checkNeighbourNodes();
    }

    private void checkNeighbourNodes()
    {
        if(currentId == goalId)
        {
            //TODO DEBUG, DELETE THIS
            foreach (KeyValuePair<int, Tile> p in tileDict)
            {
                p.Value.GetComponent<SpriteSwitcher>().setIdleSprite();
            }
            //Fill the result list
            for(int i = tileDict[currentId].ParentId; i != startId; i = tileDict[i].ParentId)
            {
                tileDict[i].GetComponent<SpriteSwitcher>().setPathSprite();
                result.Add(i);
            }
        }
        else
        {
            foreach (int i in tileDict[currentId].NeighboursIds)
                addToOpenList(i, currentId);

            //There is no solution
            if (openList.Count == 0)
                return;
            else
            {
                int min = 999;
                int minId = 0;
                foreach(int id in openList)
                    if(tileDict[id].F < min)
                    {
                        min = tileDict[id].F;
                        minId = id;
                    }
                currentId = minId;
                closedList.Add(currentId);
                openList.Remove(currentId);
                checkNeighbourNodes();
            }
        }
    }

    private void addToOpenList(int currentId, int parentId)
    {
        if (openList.Contains(currentId) || closedList.Contains(currentId))
            return;
        if (tileDict[currentId].GetComponent<OccupentHolder>().IsOccupied)
            return;
        Tile currentTile = tileDict[currentId];
        currentTile.manHattanDistance(tileDict[goalId]);
        currentTile.G = tileDict[parentId].G + 1;
        currentTile.H += tileDict[parentId].H;
        currentTile.ParentId = parentId;
        openList.Add(currentId);
    }
}
