using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using UnityEngine;
using Sirenix.OdinInspector;

public class Utility : MonoBehaviour 
{
    [RuntimeInitializeOnLoadMethod]
    public static string GetLocalIPAddress()
    {
        string localIPAddress = string.Empty;

        string hostName = Dns.GetHostName();

        IPAddress[] localIPs = Dns.GetHostAddresses(hostName);

        foreach (IPAddress ipAddress in localIPs)
        {

            if (ipAddress.AddressFamily == AddressFamily.InterNetwork)
            {
                localIPAddress = ipAddress.ToString();
                break; // Stop when the first IPv4 address is found
            }
        }
        return localIPAddress;
    }
}
