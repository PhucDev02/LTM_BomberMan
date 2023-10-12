using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Sirenix.OdinInspector;

public class UDPClient : MonoBehaviour
{
    public static UDPClient Instance;
    private void Awake()
    {
        Instance = this;
    }

    public string serverIP = "127.0.0.1";
    public int serverPort = 9000;
    private UdpClient client;
    private Thread clientThread;

    void Start()
    {
        client = new UdpClient();
        clientThread = new Thread(StartClient);
        clientThread.IsBackground = true;
        clientThread.Start();
    }

    void StartClient()
    {
        IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse(serverIP), serverPort);
        byte[] message = Encoding.ASCII.GetBytes("Hello, server!");
        client.Send(message, message.Length, serverEndPoint);


        //server tra lai
        IPEndPoint responseEndPoint = new IPEndPoint(IPAddress.Any, 0);
        byte[] response = client.Receive(ref responseEndPoint);
        string responseMessage = Encoding.ASCII.GetString(response);

        Debug.Log("Received: " + responseMessage);
    }
    [Button]
    public void SendMessageUDP(string s)
    {
        IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse(serverIP), serverPort);
        byte[] message = Encoding.ASCII.GetBytes(s);
        client.Send(message, message.Length, serverEndPoint);
    }
    void OnDestroy()
    {
        client.Close();
        clientThread.Abort();
    }
}
