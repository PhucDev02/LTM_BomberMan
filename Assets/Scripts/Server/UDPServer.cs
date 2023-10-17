using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System;

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

        private void GameLoop()
        {
            //Nhan data
            IPEndPoint clientEndPoint = new IPEndPoint(IPAddress.Any, 0);
            byte[] data = server.Receive(ref clientEndPoint);

           //Xu li data
            ProcessData(data);

            //phan hoi lai 
            byte[] response = Encoding.ASCII.GetBytes($"Received");
            server.Send(response, response.Length, clientEndPoint);
        }
        private void ProcessData(byte[] data)
        {
            Debug.Log(PlayerMovement.Get(PlayerMovement.Deserialize(data)).ToString());
            Vector2 vector2 = PlayerMovement.Get(PlayerMovement.Deserialize(data));
            PlayerServer.Instance.dir = vector2;
        }






        void OnDestroy()
        {
            server.Close();
            serverThread.Abort();
        }
    }
}