using Client;
using MainGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public FixedJoystick joystick;
    public float speed;
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
            transform.position = transform.position + speed * (Time.deltaTime * (Vector3)joystick.Direction);
            data.position.Set(transform.position);
            UDPClient.Send(data.GetBytes());
        }
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
