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
       
    }
}