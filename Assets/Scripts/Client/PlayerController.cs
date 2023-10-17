using MainGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public FixedJoystick joystick;
    public float speed;
    PlayerMovement movement;
    private void Awake()
    {
        movement = new PlayerMovement();
    }
    private void Update()
    {
        movement.Set(joystick.Direction);
        transform.position = transform.position + speed * (Time.deltaTime * (Vector3)movement.Get());
        UDPClient.Send(movement.GetBytes());
    }
}
