using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
public class PlayerMovement
{
    public float x, y;
    public void Set(Vector2 dir)
    {
        x = dir.x; y = dir.y;
    }
    public Vector2 Get()
    {
        return Vector2.right * x + Vector2.up * y;
    }
    public static Vector2 Get(PlayerMovement movement)
    {
        return Vector2.right * movement.x + Vector2.up * movement.y;
    }
    public byte[] GetBytes()
    {
        byte[] serializedData;

        using (MemoryStream memoryStream = new MemoryStream())
        {
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(memoryStream, this);
            serializedData = memoryStream.ToArray();
        }
        return serializedData;
    }
    public static PlayerMovement Deserialize(byte[] data)
    {
        PlayerMovement playerMovement;

        using (MemoryStream memoryStream = new MemoryStream(data))
        {
            IFormatter formatter = new BinaryFormatter();
            try
            {
                playerMovement = (PlayerMovement)formatter.Deserialize(memoryStream);
            }
            catch (Exception)
            {

                throw;
            }
        }

        return playerMovement;
    }
    public override string ToString()
    {
        return $"Vector2 ({x},{y}) ";
    }
}
