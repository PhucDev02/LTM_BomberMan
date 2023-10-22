using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using UnityEngine;
using System.Runtime.Serialization;

public class Utility
{
    public static Vector2 GetInitPos(PlayerColor color)
    {
        switch (color)
        {
            case PlayerColor.White:
                return new Vector2(-6, 5);
            case PlayerColor.Black:
                return new Vector2(6, 5);
            case PlayerColor.Blue:
                return new Vector2(-6, -5);
            case PlayerColor.Red:
                return new Vector2(6, -5);
        }
        return Vector2.zero;
    }
    public static PlayerColor GetPlayerColor(int index)
    {
        if (index == 1)
            return PlayerColor.White;
        if (index == 2)
            return PlayerColor.Black;
        if (index == 3)
            return PlayerColor.Blue;
        return PlayerColor.Red;
    }
    public static byte[] GetBytes(object obj)
    {
        byte[] serializedData;

        using (MemoryStream memoryStream = new MemoryStream())
        {
            IFormatter formatter = new BinaryFormatter();
            formatter.Serialize(memoryStream, obj);
            serializedData = memoryStream.ToArray();
        }

        return serializedData;
    }
    public static T Deserialize<T>(byte[] data)
    {
        T deserializedObject;

        using (MemoryStream memoryStream = new MemoryStream(data))
        {
            IFormatter formatter = new BinaryFormatter();
            try
            {
                deserializedObject = (T)formatter.Deserialize(memoryStream);
            }
            catch (Exception)
            {
                throw;
            }
        }

        return deserializedObject;
    }


}
