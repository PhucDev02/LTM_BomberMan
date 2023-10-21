using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Client
{
    public class UI_LobbyClient : UI_AbstractScreen
    {
        public override void Anim()
        {
        }

        public override void ResetAnim()
        {
        }
        public TextMeshProUGUI msg;
        public void SetMsg()
        {

        }
        public void QuitGame()
        {
            this.PostEvent(EventID.OnQuitRoom);
            LoadingSystem.Instance.LoadScene("Home", 2.0f);
        }
    }
}