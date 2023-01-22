using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum PointType
{
    None,
    Start,
    End
}

public class Point : MonoBehaviour
{
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        GameObject gridObject = GameObject.Find("Grid");
        if (gridObject == null)
            return;

        Transform tilemapObject = gridObject.transform.Find("Tilemap-Ground");
        if (tilemapObject == null)
            return;

        Tilemap tilemap = tilemapObject.GetComponent<Tilemap>();
        if (tilemap == null)
            return;

        Vector2Int c = tilemap.WorldToCell(transform.position).ToVector2();

#if UNITY_EDITOR
        UnityEditor.Handles.color = Color.white;
        UnityEditor.Handles.Label(transform.position, name + ": " + c.ToString());
#endif

        //GizmosUtils.DrawString(name + ": " + c.ToString(), transform.position);
        Gizmos.DrawWireSphere(transform.position, 0.5f);

        /*
        Vector2Int[] n = PathfindingHexGrid2D.GetNeighborCells(c);
        for (int i = 0; i < n.Length; ++i)
        {
            GizmosUtils.DrawString(i + ": " + n[i].ToString(), tilemap.CellToWorld(n[i].ToVector3()));
        }
        */
    }
#endif // UNITY_EDITOR
}
