using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_EnterIP : UI_AbstractScreen
{
    public Transform board;
    public override void Open()
    {
        base.Open();
        board.DOScale(1, 0.5f).SetEase(Ease.OutBack);
    }
    public override void Close()
    {
        board.DOScale(0, 0.5f).SetEase(Ease.InBack);
        base.Close();
    }
    public override void Anim()
    {
    }

    public override void ResetAnim()
    {
        board.localScale = Vector3.zero;
    }

    [Space(10)]
    public TMP_InputField input;
    public void EnterGame()
    {
        GameManager.serverIP = input.text;
        LoadingSystem.Instance.LoadScene("Client", 2);
    }
    public void Test()
    {

    }
}
