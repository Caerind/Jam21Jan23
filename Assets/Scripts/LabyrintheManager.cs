using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

enum HexaDirection
{
    NE = 0,
    E = 1,
    SE = 2,
    SW = 3,
    W = 4,
    NW = 5
};

public class TileInfo
{
    public Vector2Int cell;
    public bool[] wallDirection = new bool[6];
};

public class LabyrintheManager : Singleton<LabyrintheManager>
{
    public Point beginPoint;
    public Point endPoint;
    public Tilemap groundTilemap;
    public Tilemap[] wallTilemaps = new Tilemap[6];
    public TileBase[] walls = new TileBase[6];
    public float wallPercent = 20.0f;

    private Dictionary<Vector2Int, TileInfo> tileInfos = new Dictionary<Vector2Int, TileInfo>();
    private PathfindingHexGrid2D grid;

    private void Start()
    {
        grid = new PathfindingHexGrid2D(tileInfos);

        groundTilemap.CompressBounds();

        GenerateGrid();
    }

    private void GenerateGrid()
    {
        BoundsInt cellBounds = groundTilemap.cellBounds;
        for (int x = cellBounds.xMin; x < cellBounds.xMax; ++x)
        {
            for (int y = cellBounds.yMin; y < cellBounds.yMax; ++y)
            {
                Vector3Int cell = new Vector3Int(x, y, 0);

                TileInfo tileInfo = new TileInfo();
                tileInfo.cell = new Vector2Int(x, y);

                // Borders
                if (cell.x == cellBounds.xMin)
                {
                    if (cell.y % 2 == 0)
                    {
                        tileInfo.wallDirection[(int)HexaDirection.NW] = true;
                        tileInfo.wallDirection[(int)HexaDirection.W] = true;
                        tileInfo.wallDirection[(int)HexaDirection.SW] = true;
                    }
                    else
                    {
                        tileInfo.wallDirection[(int)HexaDirection.W] = true;
                    }
                }
                else if (cell.x == cellBounds.xMax - 1)
                {
                    if (cell.y % 2 == 0)
                    {
                        tileInfo.wallDirection[(int)HexaDirection.E] = true;
                    }
                    else
                    {

                        tileInfo.wallDirection[(int)HexaDirection.NE] = true;
                        tileInfo.wallDirection[(int)HexaDirection.E] = true;
                        tileInfo.wallDirection[(int)HexaDirection.SE] = true;
                    }
                }
                if (cell.y == cellBounds.yMin)
                {
                    tileInfo.wallDirection[(int)HexaDirection.SW] = true;
                    tileInfo.wallDirection[(int)HexaDirection.SE] = true;
                }
                else if (cell.y == cellBounds.yMax - 1)
                {
                    tileInfo.wallDirection[(int)HexaDirection.NW] = true;
                    tileInfo.wallDirection[(int)HexaDirection.NE] = true;
                }

                for (int i = 0; i < 6; ++i)
                {
                    // Random
                    if (!tileInfo.wallDirection[i])
                    {
                        tileInfo.wallDirection[i] = Random.Range(0.0f, 100.0f) < wallPercent;
                    }

                    wallTilemaps[i].SetTile(cell, tileInfo.wallDirection[i] ? walls[i] : null);
                }

                tileInfos.Add(tileInfo.cell, tileInfo);
            }
        }
    }

    public void SetTile(Vector2 position, TileObject tile)
    {
        if (tile == null || tile.tile == null)
            return;

        Vector3Int cell = groundTilemap.WorldToCell(position.ToVector3());
        Vector2Int cell2D = cell.ToVector2();
        TileInfo tileInfo = tileInfos.ContainsKey(cell2D) ? tileInfos[cell2D] : null;

        for (int i = 0; i < 6; ++i)
        {
            bool hasWall = tile.wallDirection[i];

            if (hasWall)
            {
                wallTilemaps[i].SetTile(cell, walls[i]);
            }
            else
            {
                wallTilemaps[i].SetTile(cell, null);
            }

            if (tileInfo != null)
            {
                tileInfo.wallDirection[i] = hasWall;
            }
        }
    }

    public Dictionary<Vector2Int, TileInfo> GetTileInfos() => tileInfos;

    public Vector2Int GetCellFromPos(Vector2 pos)
    {
        return groundTilemap.WorldToCell(pos).ToVector2();
    }

    public Vector2 GetPosFromCell(Vector2Int cell)
    {
        return groundTilemap.CellToWorld(cell.ToVector3()).ToVector2();
    }

    public void RequestPath(PathfindAStar astar, Vector2 from, Vector2 to)
    {
        Vector2Int cellStart = groundTilemap.WorldToCell(from.ToVector3()).ToVector2();
        Vector2Int cellEnd = groundTilemap.WorldToCell(to.ToVector3()).ToVector2();

        int nodeStart = grid.GetNodeFromCoords(cellStart);
        int nodeEnd = grid.GetNodeFromCoords(cellEnd);

        astar.Start(grid, nodeStart, nodeEnd);
    }

    public List<Vector2> GetPathFromAstar(PathfindAStar astar)
    {
        List<Vector2> path = new List<Vector2>();
        List<int> nodes = astar.GetPath();
        if (nodes != null)
        {
            foreach (int node in nodes)
            {
                Vector2Int cell = grid.GetCoordsFromNode(node);
                Vector2 p = groundTilemap.GetCellCenterWorld(cell.ToVector3());
                path.Add(p);
            }
        }
        return path;
    }

    public Vector2 GetStartPos() => groundTilemap.GetCellCenterWorld(groundTilemap.WorldToCell(beginPoint.transform.position.ToVector2()));

    public Vector2 GetEndPos() => groundTilemap.GetCellCenterWorld(groundTilemap.WorldToCell(endPoint.transform.position.ToVector2()));
}
