using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class UDPServer : MonoBehaviour
{
    public static UDPServer Instance;
    private void Awake()
    {
        Instance = this; 
    }

    public int port = 9000;
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
            string message = Encoding.ASCII.GetString(data);

            UIController.text = message;

            this.PostEvent(EventID.OnNewPlayer);

            //tra lai client thong diep
            Debug.Log("Received: " + message);
            byte[] response = Encoding.ASCII.GetBytes("Echo: " + message);
            server.Send(response, response.Length, clientEndPoint);
        }
    }

    void OnDestroy()
    {
        server.Close();
        serverThread.Abort();
    }
}
