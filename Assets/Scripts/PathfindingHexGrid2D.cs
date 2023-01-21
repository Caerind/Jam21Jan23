using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathfindingHexGrid2D : IGraph
{
    public class TileInfo
    {
        public Vector2Int cell;
        public TileBase type = null;
        public bool[] moveHexa = new bool[6];
    };

    private List<TileInfo> tileInfos;

    public PathfindingHexGrid2D(Tilemap tilemap, TileObjectList tileObjectList)
    {
        for (int x = tilemap.cellBounds.xMin; x <= tilemap.cellBounds.xMax; ++x)
        {
            for (int y = tilemap.cellBounds.yMin; y <= tilemap.cellBounds.yMax; ++y)
            {
                TileBase tile = tilemap.GetTile(new Vector3Int(x, y, 0));
                if (tile != null)
                {
                    TileInfo tileInfo = new TileInfo();
                    tileInfo.cell = new Vector2Int(x, y);
                    tileInfo.type = tile;

                    foreach (TileObject tileObject in tileObjectList.list)  // TODO : Opti ?
                    {
                        if (tileObject.Type == tile)
                        {
                            tileInfo.moveHexa = tileObject.movehexa;
                        }
                    }

                    tileInfos.Add(tileInfo);
                }
            }
        }
    }

    public override List<NodeLink> GetNeighbors(int node)
    {
        TileInfo tile = tileInfos[node];
        List<NodeLink> links = new List<NodeLink>();

        Vector2Int[] n = GetNeighborCells(tile.cell);
        for (int dir = 0; dir < 6; ++dir)
        {
            if (tile.moveHexa[dir])
            {
                int nodeDest = -1;
                for (int i = 0; i < tileInfos.Count; ++i) // TODO : Opti
                {
                    if (tileInfos[i].cell == n[dir])
                    {
                        nodeDest = i;
                        break;
                    }
                }
                if (nodeDest >= 0)
                {
                    links.Add(new NodeLink(node, nodeDest, 1.0f));
                }
            }
        }
        return links;
    }

    public override bool IsValidNode(int node)
    {
        return node >= 0 && node < tileInfos.Count;
    }

    public static Vector2Int[] GetNeighborCells(Vector2Int cell)
    {
        Vector2Int[] n = new Vector2Int[6];
        if (cell.y % 2 == 0)
        {
            n[0] = new Vector2Int(cell.x - 1, cell.y - 1);
            n[1] = new Vector2Int(cell.x, cell.y - 1);
            n[2] = new Vector2Int(cell.x + 1, cell.y);
            n[3] = new Vector2Int(cell.x, cell.y + 1);
            n[4] = new Vector2Int(cell.x - 1, cell.y + 1);
            n[5] = new Vector2Int(cell.x - 1, cell.y);
        }
        else
        {
            n[0] = new Vector2Int(cell.x, cell.y - 1);
            n[1] = new Vector2Int(cell.x + 1, cell.y - 1);
            n[2] = new Vector2Int(cell.x + 1, cell.y);
            n[3] = new Vector2Int(cell.x + 1, cell.y + 1);
            n[4] = new Vector2Int(cell.x, cell.y + 1);
            n[5] = new Vector2Int(cell.x - 1, cell.y);
        }
        return n;
    }
}
