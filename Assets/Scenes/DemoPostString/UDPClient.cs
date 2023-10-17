using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using TMPro;
using UnityEngine;

namespace PostString
{
    public class UDPClient : MonoBehaviour
    {
        public static string serverIP ;
        public static int serverPort = 9000;
        private static UdpClient client;
        private Thread clientThread;

        void Start()
        {
            serverIP= Utility.GetLocalIPAddress();
            Debug.Log(serverIP);
            clientThread = new Thread(StartClient);
            clientThread.IsBackground = true;
            clientThread.Start();
        }

        void StartClient()
        {
            client = new UdpClient();
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse(serverIP), serverPort);
            byte[] message = Encoding.ASCII.GetBytes("Hello");
            client.Send(message, message.Length, serverEndPoint);


            //IPEndPoint responseEndPoint = new IPEndPoint(IPAddress.Any, 0);
            //byte[] response = client.Receive(ref responseEndPoint);
            //string responseMessage = Encoding.ASCII.GetString(response);
        }
        [Button]
        public static void SendMessageUDP(string s)
        {
            //try
            //{
                IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse(serverIP), serverPort);
                byte[] message = Encoding.ASCII.GetBytes(s);
                client.Send(message, message.Length, serverEndPoint);


                //IPEndPoint responseEndPoint = new IPEndPoint(IPAddress.Any, 0);
                //byte[] response = client.Receive(ref responseEndPoint); 
                //string responseMessage = Encoding.ASCII.GetString(response);

                //Debug.Log("Client Received: " + responseMessage);
            //}
            //catch (System.Exception e)
            //{

            //    Debug.LogException(e);
            //}
        }
        public static void Send(byte[] s)
        {
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse(serverIP), serverPort);
            client.Send(s, s.Length, serverEndPoint);
        }
        void OnDestroy()
        {
            client.Close();
            clientThread.Abort();
        }

        public TMP_InputField field;
        public void OnClick()
        {
            SendMessageUDP(field.text);
        }
    }
}