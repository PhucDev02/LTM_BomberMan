using Client;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerDataPacket
{
   public static List<ClientDataPacket> clientsData = new List<ClientDataPacket>();
    public static ServerConfig config;
}
