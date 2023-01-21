using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


[CreateAssetMenu(fileName = "New Tile object List", menuName = "Tile Object List")]
public class TileObjectList : ScriptableObject
{
    public List<TileObject> list;
}

