using Client;
using Common;
using MainGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Config")]
    public PlayerColor color;
    public string ip;

    ClientDataPacket data;
    private void Awake()
    {
        data = new ClientDataPacket();
        data.config.isOnline = true;
        this.RegisterListener(EventID.OnQuitRoom, (res) => QuitRoom());
    }
    private void Update()
    {
        if (data.config.isOnline)
        {
            data.position.Set(transform.position);
            UDPClient.Send(data.GetBytes());
        }
    }
    public void SetPosition(Vector2 pos)
    {
        data.position.Set(transform.position);
    }
    private void QuitRoom()
    {
        if (data.config.isOnline)
        {
            data.config.isOnline = false;
            UDPClient.Send(data.GetBytes());
            Debug.Log("Client Quit Room");
        }
    }
}
