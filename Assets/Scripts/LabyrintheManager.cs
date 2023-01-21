using UnityEngine;
using UnityEngine.Tilemaps;

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
}
