using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Client
{
    [Serializable]
    public class ClientDataPacket
    {
       public PlayerPosition position;
       public PlayerConfig config;

       public ClientDataPacket()
        {
            position = new PlayerPosition();
            config = new PlayerConfig();
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
        public static ClientDataPacket Deserialize(byte[] data)
        {
            ClientDataPacket playerMovement;

            using (MemoryStream memoryStream = new MemoryStream(data))
            {
                IFormatter formatter = new BinaryFormatter();
                try
                {
                    playerMovement = (ClientDataPacket)formatter.Deserialize(memoryStream);
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return playerMovement;
        }
    }
}