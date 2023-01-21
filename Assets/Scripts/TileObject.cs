using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


[CreateAssetMenu (fileName = "New Tile object", menuName = "Tile Object" )]
public class TileObject : ScriptableObject
{
    public TileBase Type;
    public bool[] movehexa = new bool[6];
    public bool isStartTile = false;
    public bool isEndTile = false;
}
