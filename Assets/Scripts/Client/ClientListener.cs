using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class ClientListener : MonoBehaviour
{
    #region declare port and server
    public static int port = 9000;
    private UdpClient client;
    private Thread listenThread;
    #endregion
    #region start server
    void Start()
    {
        port = 9000;
        listenThread = new Thread(StartServer);
        listenThread.IsBackground = true;
        listenThread.Start();
    }
    void StartServer()
    {
        client = new UdpClient(port);
        while (true)
        {
            Loop();
        }
    }

    #endregion
    private void Loop()
    {
        //Nhan data
        IPEndPoint clientEndPoint = new IPEndPoint(IPAddress.Any, 0);
        byte[] bytes = client.Receive(ref clientEndPoint);

        //Xu li data
        ProcessData(bytes);

        //phan hoi lai 
        //byte[] response = Encoding.ASCII.GetBytes($"Received");
        //client.Send(response, response.Length, clientEndPoint);
    }

    private void ProcessData(byte[] bytes)
    {
        //throw new NotImplementedException();
    }
    private void OnDestroy()
    {
        client.Close();
        listenThread.Abort();
    }
}
