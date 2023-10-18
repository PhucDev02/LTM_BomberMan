using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Home : MonoBehaviour
{
    public void EnterGame()
    {
        LoadingSystem.Instance.LoadScene("Client",2);
    }
    public void Test()
    {

    }
}
