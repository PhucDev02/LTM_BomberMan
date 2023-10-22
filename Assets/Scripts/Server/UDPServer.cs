using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System;
using System.Collections.Generic;
using Client;
using Server;

namespace MainGame
{
    public class UDPServer : MonoBehaviour
    {
        #region declare port and server
        public static int port = 9000;
        private UdpClient server;
        private Thread serverThread;
        #endregion
        #region start server
        void Start()
        {
#if UNITY_EDITOR
            port = 9000;
            serverThread = new Thread(StartServer);
            serverThread.IsBackground = true;
            serverThread.Start();
#endif
        }
        void StartServer()
        {
            server = new UdpClient(port);
            while (true)
            {
                GameLoop();
            }
        }
        #endregion

        public static Dictionary<string, IPEndPoint> clients = new Dictionary<string, IPEndPoint>();
        private void GameLoop()
        {
            //Nhan data
            IPEndPoint clientEndPoint = new IPEndPoint(IPAddress.Any, 0);
            byte[] bytes = server.Receive(ref clientEndPoint);

            ProcessNewClient(clientEndPoint);
            //Xu li data
            ProcessData(bytes, clientEndPoint);

            //phan hoi lai 
            //byte[] response = Encoding.ASCII.GetBytes($"Received");
            //server.Send(response, response.Length, clientEndPoint);
        }

        private void ProcessNewClient(IPEndPoint clientEndPoint)
        {

            //int senderPort = clientEndPoint.Port;
            //string senderIp = clientEndPoint.Address.ToString();
            //string playerName = senderIp + ":" + senderPort;
            string playerName = clientEndPoint.Address.ToString();
            if (!clients.ContainsKey(playerName))
            {
                if (clients.Count >= 4)
                {
                    Debug.LogError("Room is full");
                    return;
                }
                Debug.Log(playerName);
                clients.Add(playerName, clientEndPoint);
                UnityMainThreadDispatcher.Instance().Enqueue(() =>
                {
                    PlayerManager.GeneratePlayer(playerName);
                });

            }
        }

        private void ProcessData(byte[] bytes, IPEndPoint clientEndPoint)
        {
            ClientDataPacket data = Utility.Deserialize<ClientDataPacket>(bytes);


            if (!data.config.isOnline)
            {
                UnityMainThreadDispatcher.Instance().Enqueue(() =>
                {
                    UI_Lobby.Instance.RemoveClient(clientEndPoint.Address.ToString());
                    PlayerManager.RemovePlayer(clientEndPoint.Address.ToString());
                    clients.Remove(clientEndPoint.Address.ToString());
                });
            }
        }

        void OnDestroy()
        {
            server.Close();
            serverThread.Abort();
        }
    }
}