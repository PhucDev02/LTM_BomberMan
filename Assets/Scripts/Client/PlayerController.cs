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
    }
    private void Update()
    {
        data.position.Set(joystick.Direction);
        transform.position = transform.position + speed * (Time.deltaTime * (Vector3)joystick.Direction);
        UDPClient.Send(data.GetBytes());
    }
    private void OnDestroy()
    {
        data.config.isOnline = false;
        UDPClient.Send(data.GetBytes());
    }
}
