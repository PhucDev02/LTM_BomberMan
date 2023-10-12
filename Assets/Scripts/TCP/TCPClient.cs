using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class TCPClient : MonoBehaviour
{
    private TcpClient client;
    private NetworkStream stream;

    // Địa chỉ IP và cổng của máy chủ
    public string serverIP = "127.0.0.1";
    public int serverPort = 8888;

    private void Start()
    {
        // Kết nối đến máy chủ
        client = new TcpClient();
        client.Connect(serverIP, serverPort);
        Debug.Log("Connected to server");

        // Lấy luồng mạng để gửi và nhận dữ liệu
        stream = client.GetStream();

        // Gửi dữ liệu đến máy chủ
        string message = "Hello, server!";
        byte[] messageBytes = Encoding.ASCII.GetBytes(message);
        stream.Write(messageBytes, 0, messageBytes.Length);
        Debug.Log("Sent: " + message);
    }

    private void OnDestroy()
    {
        // Đóng kết nối khi đối tượng bị hủy
        stream.Close();
        client.Close();
    }
}
