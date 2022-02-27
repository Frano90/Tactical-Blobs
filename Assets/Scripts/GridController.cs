using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GridController : MonoBehaviour
{
    [SerializeField] private float width, height;
    [SerializeField] private Tile tile_pf;
    //[SerializeField] private Camera cam;
    [SerializeField] private Transform tileContainer;

    private Dictionary<Vector2, Tile> grid = new Dictionary<Vector2, Tile>();
    private Dictionary<GridObject, Vector2> unitsCoordenates = new Dictionary<GridObject, Vector2>();

    public Dictionary<Vector2, Tile> GetGrid()
    {
        return grid;
    }
    public Dictionary<GridObject, Vector2> GetUnitCoordenates()
    {
        return unitsCoordenates;
    }

    public bool ThisTileExist(int x, int y)
    {
        return x >= width || x < 0 || y >= height || y < 0 ? false : true;
    }

    public void AddNewObjectToTheGrid(GridObject newGridObject, Vector2 pos)
    {
        unitsCoordenates.Add(newGridObject, pos);
    }
    void Start()
    {
        CreateGrid();
        //LocateCamera();

    }

    // private void LocateCamera()
    // {
    //     cam.transform.position = new Vector3((float) width / 2 - 0.5f, 5.5f,(float) height / 2 - 0.5f);
    // }

    public Tile GetRandomTile()
    {
        int x = (int)Random.Range(0, width);
        int y = (int)Random.Range(0, height);

        Debug.Log(x + " " + y);
        
        return grid[new Vector2(x, y)];
    }

    void CreateGrid()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Tile newTile = Instantiate(tile_pf, new Vector3(i, 0, j), Quaternion.identity);
                newTile.SetGridPosition(i, j);
                newTile.name = $"Tile {i} - {j}";
                newTile.transform.SetParent(tileContainer);
                
                

                bool isOffset = (i % 2 == 0 && j % 2 != 0 || i % 2 != 0 && j % 2 == 0);
                newTile.Init(isOffset);
                
                grid.Add(new Vector2(i,j), newTile);
                
            }
        }
    }

    public void UpdateUnitPosition(GridObject unit, Vector2 currentUnitPos, Vector2 desiredDestination)
    {
        grid[currentUnitPos].SetOccupied(false);
        grid[desiredDestination].SetOccupied(true);

        unitsCoordenates[unit] = desiredDestination;
    }
}
