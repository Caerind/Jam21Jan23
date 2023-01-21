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
    private Tilemap tilemap;

    private void Awake()
    {
        tilemap = GetComponentInChildren<Tilemap>();
    }

    private void Start()
    {
        
    }

    public void SetTile(Vector2 position, TileBase tile)
    {
        Vector3Int cell = tilemap.WorldToCell(position.ToVector3());
        tilemap.SetTile(cell, tile);
    }

    public TileBase GetTile(Vector2 position)
    {
        Vector3Int cell = tilemap.WorldToCell(position.ToVector3());
        return tilemap.GetTile(cell);
    }

    public static Vector3Int[] GetNeighborCells(Vector3Int cell)
    {
        Vector3Int[] n = new Vector3Int[6];
        if (cell.y % 2 == 0)
        {
            n[0] = new Vector3Int(cell.x - 1, cell.y - 1, 0);
            n[1] = new Vector3Int(cell.x, cell.y - 1, 0);
            n[2] = new Vector3Int(cell.x + 1, cell.y, 0);
            n[3] = new Vector3Int(cell.x, cell.y + 1, 0);
            n[4] = new Vector3Int(cell.x - 1, cell.y + 1, 0);
            n[5] = new Vector3Int(cell.x - 1, cell.y, 0);
        }
        else
        {
            n[0] = new Vector3Int(cell.x, cell.y - 1, 0);
            n[1] = new Vector3Int(cell.x + 1, cell.y - 1, 0);
            n[2] = new Vector3Int(cell.x + 1, cell.y, 0);
            n[3] = new Vector3Int(cell.x + 1, cell.y + 1, 0);
            n[4] = new Vector3Int(cell.x, cell.y + 1, 0);
            n[5] = new Vector3Int(cell.x - 1, cell.y, 0);
        }
        return n;
    }
}
