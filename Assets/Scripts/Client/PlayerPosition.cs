using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
public class PlayerPosition
{
    public float x, y;
    public void Set(Vector2 pos)
    {
        x = pos.x; y = pos.y;
    }
    public Vector2 Get()
    {
        return Vector2.right * x + Vector2.up * y;
    }
    public static Vector2 Get(PlayerPosition pos)
    {
        return Vector2.right * pos.x + Vector2.up * pos.y;
    }
    public override string ToString()
    {
        return $"Vector2 ({x},{y}) ";
    }
}
