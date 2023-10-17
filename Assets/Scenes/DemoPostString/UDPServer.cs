using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Text;
using UnityEngine;
using System.Threading;
using System.Linq;
using TMPro;
using Sirenix.OdinInspector;

namespace PostString
{
    public class UDPServer : MonoBehaviour
    {
        private int port = 9000;
        private UdpClient server;
        private Thread serverThread;

        void Start()
        {
#if UNITY_EDITOR
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
                IPEndPoint clientEndPoint = new IPEndPoint(IPAddress.Any, 0);
                byte[] data = server.Receive(ref clientEndPoint);

                string message= Encoding.ASCII.GetString(data);

                s = Reverse(message);
                Debug.Log("Server receive: " + message);
                //byte[] response = Encoding.ASCII.GetBytes(""+Reverse(message));
                //server.Send(response, response.Length, clientEndPoint);
            }
        }

        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
        [Button]
        void OnDestroy()
        {
            server.Close();
            serverThread.Abort();
        }
        public TextMeshProUGUI text;
        public static string s;
        private void Update()
        {
            text.text = s;
        }
    }
}