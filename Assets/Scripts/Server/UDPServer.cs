using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System;
using System.Collections.Generic;
using Client;

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

        public static Dictionary<IPEndPoint, string> clients = new Dictionary<IPEndPoint, string>();
        private void GameLoop()
        {
            //Nhan data
            IPEndPoint clientEndPoint = new IPEndPoint(IPAddress.Any, 0);
            byte[] bytes = server.Receive(ref clientEndPoint);

            ProcessNewClient(clientEndPoint);
            //Xu li data
            ProcessData(bytes);

            //phan hoi lai 
            byte[] response = Encoding.ASCII.GetBytes($"Received");
            server.Send(response, response.Length, clientEndPoint);
        }

        private void ProcessNewClient(IPEndPoint clientEndPoint)
        {

            if (!clients.ContainsKey(clientEndPoint))
            {
                int senderPort = clientEndPoint.Port;
                string senderIp = clientEndPoint.Address.ToString();
                string playerKey = senderIp + ":" + senderPort;

                clients.Add(clientEndPoint,playerKey);
                UnityMainThreadDispatcher.Instance().Enqueue(() =>
                {
                    Instantiate(Resources.Load<GameObject>("Player"));
                }); 
            }
        }

        private void ProcessData(byte[] bytes)
        {
            ClientDataPacket data = ClientDataPacket.Deserialize(bytes);


            PlayerServer.Instance.SetPositon(data.position.Get());

            if (data.config.isOnline == false)
            {
                Destroy(PlayerServer.Instance.gameObject);
                //clients.Remove()
                Debug.Log("destroy"); 
            }
        }

        void OnDestroy()
        {
            server.Close();
            serverThread.Abort();
        }
    }
}