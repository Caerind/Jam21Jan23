using UnityEngine;

public static class VectorExtenstions
{
    public static Vector2 ToVector2(this Vector3 v)
    {
        return new Vector2(v.x, v.y);
    }

    public static Vector2 xy(this Vector3 v)
    {
        return new Vector2(v.x, v.y);
    }

    public static Vector2 xz(this Vector3 v)
    {
        return new Vector2(v.x, v.z);
    }

    public static Vector3 ToVector3(this Vector2 v, float z = 0.0f)
    {
        return new Vector3(v.x, v.y, z);
    }

    public static Vector2Int ToVector2(this Vector3Int v)
    {
        return new Vector2Int(v.x, v.y);
    }

    public static Vector2Int xy(this Vector3Int v)
    {
        return new Vector2Int(v.x, v.y);
    }

    public static Vector2Int xz(this Vector3Int v)
    {
        return new Vector2Int(v.x, v.z);
    }

    public static Vector3Int ToVector3(this Vector2Int v, int z = 0)
    {
        return new Vector3Int(v.x, v.y, z);
    }
}