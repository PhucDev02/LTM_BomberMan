using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class ByteConverter : MonoBehaviour
{
    public static byte[] GetBytes(PlayerPosition obj)
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
    public static PlayerPosition FromBytes(byte[] data)
    {
        PlayerPosition playerMovement;

        using (MemoryStream memoryStream = new MemoryStream(data))
        {
            IFormatter formatter = new BinaryFormatter();
            playerMovement = (PlayerPosition)formatter.Deserialize(memoryStream);
        }

        return playerMovement;
    }
}
