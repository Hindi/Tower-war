    using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapGenerator : MonoBehaviour
{
    private GameObject hexagonMiddlePrefab;
    private GameObject hexagonBottomPrefab;
    private GameObject hexagonTopPrefab;
    private GameObject hexagonRightPrefab;
    private GameObject hexagonFarRightPrefab;
    private GameObject hexagonLeftPrefab;
    private GameObject hexagonFarLeftPrefab;
    List<List<Vector2> > tilePositions;

    void Start()
    {
        tilePositions = new List<List<Vector2> >();
        TextAsset csvFile = Resources.Load("map") as TextAsset;
        string[,] grid = CSVReader.SplitCsvGrid(csvFile.text);

        hexagonMiddlePrefab = Resources.Load("Tile") as GameObject;
        hexagonBottomPrefab = Resources.Load("TileBot") as GameObject;
        hexagonTopPrefab = Resources.Load("TileTop") as GameObject;
        hexagonRightPrefab = Resources.Load("TileRight") as GameObject;
        hexagonFarRightPrefab = Resources.Load("TileFarRight") as GameObject;
        hexagonLeftPrefab = Resources.Load("TileLeft") as GameObject;
        hexagonFarLeftPrefab = Resources.Load("TileFarLeft") as GameObject;

        preProcessMap(grid);
        generateMap();
    }

    private void preProcessMap(string[,] grid)
    {
        for (int y = 0; y < grid.GetLength(1); y++)
        {
            tilePositions.Add(new List<Vector2>());
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                if (grid[x,y] == "x")
                {
                    tilePositions[y].Add(new Vector2(x,- y));
                }
            }
        }
    }

    // outputs the content of a 2D array, useful for checking the importer
    public void generateMap()
    {
        float offsetHeight = 1.7f;
        float offsetWidth = 0.95f;
        float offsetInLine = 1.95f;

        foreach(List<Vector2> row in tilePositions)
        {
            foreach(Vector2 pos in row)
            {
                float offW = (pos.y % 2 == 0 ? 0 : offsetWidth);
                GameObject.Instantiate(hexagonMiddlePrefab, new Vector3(pos.x * offsetInLine + offW, 0, pos.y * offsetHeight), Quaternion.identity);
            }
        }

       /* GameObject prefab;
        int lastRow = tilePositions.Count - 1;
        int lastCol = grid.GetLength(0) - 1;
        for (int y = 0; y < grid.GetLength(1); y++)
        {
            float offW = (y % 2 == 0 ? 0 : offsetWidth);
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                if (y == 0)
                    prefab = hexagonBottomPrefab;
                else if (y == lastRow)
                    prefab = hexagonTopPrefab;
                else
                {
                    if (x == lastCol)
                    {
                        if (offW != 0)
                            prefab = hexagonFarRightPrefab;
                        else
                            prefab = hexagonRightPrefab;
                    }
                    else if (x == 0)
                    {
                        if (offW == 0)
                            prefab = hexagonFarLeftPrefab;
                        else
                            prefab = hexagonLeftPrefab;
                    }
                    else
                        prefab = hexagonMiddlePrefab;
                }
                GameObject.Instantiate(prefab, new Vector3(x * offsetInLine + offW, 0, y * offsetHeight), Quaternion.identity);
            }
        }*/
    }
}
