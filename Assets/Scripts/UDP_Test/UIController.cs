using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController Instance;
    private void Awake()
    {
        Instance = this;
        this.RegisterListener(EventID.OnNewPlayer, (x) => SpawnPlayer());
#if UNITY_EDITOR
#else
    txt.gameObject.SetActive(false);
#endif
    }
    public GameObject obj;
    private void SpawnPlayer()
    {
        Instantiate(obj);
    }

    public TMP_InputField input;
    public TextMeshProUGUI txt;

    public void SubmitToServer()
    {
        UDPClient.Instance.SendMessageUDP(input.text);
    }
    public static string text;
    public void Update()
    {
        txt.text = text;
    }
    public void UpdateMessage(string s)
    {
        txt.text = s;
    }
}
