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

        // Get the host name of the local machine
        string hostName = Dns.GetHostName();

        // Get the IP addresses associated with the local machine
        IPAddress[] localIPs = Dns.GetHostAddresses(hostName);

        // Loop through the IP addresses and find a suitable one
        foreach (IPAddress ipAddress in localIPs)
        {
            // Check if the IP address is an IPv4 address (not IPv6)
            if (ipAddress.AddressFamily == AddressFamily.InterNetwork)
            {
                localIPAddress = ipAddress.ToString();
                break; // Stop when the first IPv4 address is found
            }
        }

        return localIPAddress;
    }
}
