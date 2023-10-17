using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerServer : MonoBehaviour
{
    public static PlayerServer Instance;
    public void Awake()
    {
        Instance = this;
        dir = new Vector2();
    }

    public Vector2 dir;
    private void Update()
    {
        transform.position = transform.position + (Vector3)dir * 5 * Time.deltaTime;
    }
}
