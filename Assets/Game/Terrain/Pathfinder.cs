using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Zone))]
public class Pathfinder : MonoBehaviour {

    private Zone zone;

    private Dictionary<int, Tile> tileDict;
    private List<int> openList;
    private List<int> closedList;
    private List<Vector3> result;
    public List<Vector3> Result
    {
        get { return result; }
    }

    private int startId;
    public int StartId
    {
        get { return startId; }
        set { startId = value; }
    }

    private int goalId;
    public int GoalId
    {
        get { return goalId; }
        set { goalId = value; }
    }

    private int currentId;

    void Awake()
    {
        zone = GetComponent<Zone>();
        result = new List<Vector3>();
        closedList = new List<int>();
        openList = new List<int>();

        EventManager.AddListener(EnumEvent.TILEMAPUPDATE, onMapUpdate);
    }

    void OnDestroy()
    {
        EventManager.RemoveListener(EnumEvent.TILEMAPUPDATE, onMapUpdate);
    }

    public void onMapUpdate()
    {
        findPath();
    }

    private void initialiseZone(List<Vector3> path)
    {
        tileDict = zone.TileDict;
        openList.Clear();
        closedList.Clear();
        path.Clear();

        currentId = startId;
        closedList.Add(startId);
    }

    public bool canAddObstacle(Tile tile)
    {
        if (tile.Id == startId || tile.Id == goalId)
            return false;
        List<Vector3> path = new List<Vector3>();

        initialiseZone(path);
        bool wasOccupied = tileDict[tile.Id].GetComponent<OccupentHolder>().IsOccupied;
        tileDict[tile.Id].GetComponent<OccupentHolder>().IsOccupied = true;
        bool res = checkNeighbourNodes(path);

        tileDict[tile.Id].GetComponent<OccupentHolder>().IsOccupied = wasOccupied;
        return res;
    }

    public List<Vector3> findPathFromPosition(int tileId)
    {
        List<Vector3> path = new List<Vector3>();
		int saveStartId = startId;
        var saveresult = result;

        startId = tileId;
        initialiseZone(path);

        if (!checkNeighbourNodes(path))
            path.Add(tileDict[goalId].transform.position);

        startId = saveStartId;
        result = saveresult;

        return path;
    }

    public void findPath()
    {
        initialiseZone(result);
        checkNeighbourNodes(result);
    }

    private bool checkNeighbourNodes(List<Vector3> path)
    {
        if(currentId == goalId)
        {
            //TODO DEBUG, DELETE THIS
            foreach (KeyValuePair<int, Tile> p in tileDict)
            {
                p.Value.GetComponent<SpriteSwitcher>().setIdleSprite();
            }
            //Fill the result list
            for(int i = tileDict[currentId].Id; i != startId; i = tileDict[i].ParentId)
            {
                //TODO DEBUG, DELETE THIS
                tileDict[i].GetComponent<SpriteSwitcher>().setPathSprite();
                path.Add(tileDict[i].transform.position);
            }

            path.Reverse();
            return true;
        }
        else
        {
            foreach (int i in tileDict[currentId].NeighboursIds)
                addToOpenList(i, currentId);

            //There is no solution
            if (openList.Count == 0)
                return false;
            else
            {
                int min = 999999;
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
                return checkNeighbourNodes(path);
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
