using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

enum HexaDirection
{
    NE,
    E,
    SE,
    SW,
    W,
    NW
};

public class LabyrintheManager : Singleton<LabyrintheManager> 
{
    public TileObjectList wallTileList;
    public Vector2Int beginCoords;
    public Vector2Int endCoords;

    private Tilemap tilemap;
    private PathfindingHexGrid2D grid;

    private void Awake()
    {
        tilemap = GetComponentInChildren<Tilemap>();
    }

    private void Start()
    {
        InitGrid();
    }

    private void InitGrid()
    {
        grid = new PathfindingHexGrid2D(tilemap, wallTileList, beginCoords, endCoords);
    }

    public void SetTile(Vector2 position, TileBase tile)
    {
        Vector3Int cell = tilemap.WorldToCell(position.ToVector3());
        tilemap.SetTile(cell, tile);
        InitGrid(); // TODO : Opti
    }

    public TileBase GetTile(Vector2 position)
    {
        Vector3Int cell = tilemap.WorldToCell(position.ToVector3());
        return tilemap.GetTile(cell);
    }

    public List<Vector2> GetPath(Vector2 from, Vector2 to)
    {
        Vector2Int cellStart = tilemap.WorldToCell(from.ToVector3()).ToVector2();
        Vector2Int cellEnd = tilemap.WorldToCell(to.ToVector3()).ToVector2();

        int nodeStart = grid.GetTileInfoIndex(cellStart);
        int nodeEnd = grid.GetTileInfoIndex(cellEnd);

        if (nodeStart >= 0 && nodeEnd >= 0)
        {
            PathfindAStar astar = new PathfindAStar();
            astar.Start(grid, nodeStart, nodeEnd);
            while (!astar.IsFinished())
            {
                astar.Iteration();
            }

            List<Vector2> path = new List<Vector2>();
            List<int> nodes = astar.GetPath();
            foreach (int node in nodes)
            {
                Vector2Int cell = grid.GetTileInfoFromIndex(node).cell;
                Vector2 p = tilemap.GetCellCenterWorld(cell.ToVector3());
                path.Add(p);
            }
            return path;
        }

        return null;
    }

    public Vector2 GetStartPos()
    {
        PathfindingHexGrid2D.TileInfo start = grid.GetStartTile();
        if (start != null)
        {
            return tilemap.GetCellCenterWorld(start.cell.ToVector3());
        }
        return Vector2.zero;
    }

    public Vector2 GetEndPos()
    {
        PathfindingHexGrid2D.TileInfo end = grid.GetEndTile();
        if (end != null)
        {
            return tilemap.GetCellCenterWorld(end.cell.ToVector3());
        }
        return Vector2.zero;
    }
}
