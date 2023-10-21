using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerServer : MonoBehaviour
{
    public static PlayerServer Instance;
    public void Awake()
    {
        Instance = this;
    }
    public string pName;
    public void SetPositon(Vector2 pos)
    {
        UnityMainThreadDispatcher.Instance().Enqueue(() =>
        {
            transform.position = pos;
        });
    }
}
