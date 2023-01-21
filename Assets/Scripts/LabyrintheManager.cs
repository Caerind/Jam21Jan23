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
    public TileObjectList tileObjectList;

    private Tilemap tilemap;
    private PathfindingHexGrid2D grid;

    private void Awake()
    {
        tilemap = GetComponentInChildren<Tilemap>();
    }

    private void Start()
    {
        
    }

    private void InitGrid()
    {
        grid = new PathfindingHexGrid2D(tilemap, tileObjectList);
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
}
