using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class TCPServer : MonoBehaviour
{

    public static TCPServer Instance;
    private void Awake()
    {
        Instance = this;
    }

    private TcpListener server;
    private TcpClient client;
    private NetworkStream stream;
    private byte[] receiveBuffer;

    // Port mà máy chủ lắng nghe
    public int port = 8888;

    private void Start()
    {
        // Khởi tạo máy chủ TCP
        server = new TcpListener(IPAddress.Any, port);
        server.Start();
        Debug.Log("Server started on port " + port);

        // Chờ kết nối từ máy khách
        client = server.AcceptTcpClient();
        Debug.Log("Client connected");

        // Lấy luồng mạng để gửi và nhận dữ liệu
        stream = client.GetStream();
        receiveBuffer = new byte[1024];

        // Bắt đầu đọc dữ liệu từ máy khách
        stream.BeginRead(receiveBuffer, 0, receiveBuffer.Length, OnReceivedData, null);
    }

    private void OnReceivedData(IAsyncResult ar)
    {
        int bytesRead = stream.EndRead(ar);
        if (bytesRead <= 0)
        {
            Debug.Log("Client disconnected");
            return;
        }

        // Xử lý dữ liệu nhận được từ máy khách
        string receivedMessage = Encoding.ASCII.GetString(receiveBuffer, 0, bytesRead);
        Debug.Log("Received: " + receivedMessage);

        // Tiếp tục đọc dữ liệu
        stream.BeginRead(receiveBuffer, 0, receiveBuffer.Length, OnReceivedData, null);
    }

    private void OnDestroy()
    {
        // Đóng tất cả kết nối khi đối tượng bị hủy
        stream.Close();
        client.Close();
        server.Stop();
    }
}
