using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PathfindingHexGrid2D : IGraph
{
    private Dictionary<Vector2Int, TileInfo> tileInfos = null;

    public PathfindingHexGrid2D(Dictionary<Vector2Int, TileInfo> tileInfos)
    {
        this.tileInfos = tileInfos;
    }

    public override List<NodeLink> GetNeighbors(int node)
    {
        List<NodeLink> links = new List<NodeLink>();

        Vector2Int coords = GetCoordsFromNode(node);
        TileInfo tile = tileInfos[coords];
        if (tile == null)
            return null;

        Vector2Int[] neighbors = GetNeighborCells(tile.cell);
        for (int dir = 0; dir < 6; ++dir)
        {
            if (tile.wallDirection[dir] == false)
            {
                Vector2Int otherCoords = neighbors[dir];
                TileInfo otherTile = tileInfos.ContainsKey(otherCoords) ? tileInfos[otherCoords] : null;

                if (otherTile != null && otherTile.wallDirection[(dir + 3) % 6] == false)
                {
                    int otherNode = GetNodeFromCoords(otherCoords);
                    float cost = 1.0f + Random.Range(-0.005f, 0.005f);
                    links.Add(new NodeLink(node, otherNode, cost));
                }
            }
        }
        return links;
    }

    public override bool IsValidNode(int node)
    {
        return tileInfos.ContainsKey(GetCoordsFromNode(node));
    }
    public int GetNodeFromCoords(Vector2Int coords)
    {
        return (coords.y + minusHandler) * hash + (coords.x + minusHandler);
    }
    public Vector2Int GetCoordsFromNode(int node)
    {
        int x = node % hash;
        int y = node / hash;
        return new Vector2Int(x - minusHandler, y - minusHandler);
    }

    // Internal

    private static int hash = 10000;
    private static int minusHandler = 100;

    public static Vector2Int[] GetNeighborCells(Vector2Int cell)
    {
        Vector2Int[] n = new Vector2Int[6];
        if (cell.y % 2 == 0)
        {
            n[(int)HexaDirection.NE] = new Vector2Int(cell.x, cell.y + 1);
            n[(int)HexaDirection.E] = new Vector2Int(cell.x + 1, cell.y);
            n[(int)HexaDirection.SE] = new Vector2Int(cell.x, cell.y - 1);
            n[(int)HexaDirection.SW] = new Vector2Int(cell.x - 1, cell.y - 1);
            n[(int)HexaDirection.W] = new Vector2Int(cell.x - 1, cell.y);
            n[(int)HexaDirection.NW] = new Vector2Int(cell.x - 1, cell.y + 1);
        }
        else
        {
            n[(int)HexaDirection.NE] = new Vector2Int(cell.x + 1, cell.y + 1);
            n[(int)HexaDirection.E] = new Vector2Int(cell.x + 1, cell.y);
            n[(int)HexaDirection.SE] = new Vector2Int(cell.x + 1, cell.y - 1);
            n[(int)HexaDirection.SW] = new Vector2Int(cell.x, cell.y - 1);
            n[(int)HexaDirection.W] = new Vector2Int(cell.x - 1, cell.y);
            n[(int)HexaDirection.NW] = new Vector2Int(cell.x, cell.y + 1);
        }
        return n;
    }
}
