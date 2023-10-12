using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDispatcher : MonoBehaviour
{
    public static PlayerDispatcher Instance;
    private void Awake()
    {
        Instance = this;
    }

}
