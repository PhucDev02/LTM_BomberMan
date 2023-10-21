using Common;
using MainGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Server
{
    public class PlayerManager : MonoBehaviour
    {
        public static List<GameObject> players=new List<GameObject>();
        public static void GeneratePlayer(string playerName)
        {
           
            PlayerColor color = Utility.GetPlayerColor(UDPServer.clients.Count);
            Debug.Log(UDPServer.clients.Count);
            var newPlayer = Instantiate(Resources.Load<GameObject>("Server/" + color.ToString() + "Player"));
            newPlayer.transform.position = Utility.GetInitPos(color);
            newPlayer.GetComponent<PlayerController>().color = color;
            newPlayer.GetComponent<PlayerController>().ip = playerName;

            UI_Lobby.Instance.AddClient(playerName);
            players.Add(newPlayer);
        }
        public static void RemovePlayer(string playerName)
        {
            foreach (var player in players)
            {
                if (player.GetComponent<PlayerController>().ip == playerName)
                {
                    Destroy(player.gameObject);
                    players.Remove(player);
                    return;
                }
            }
        }
    }
}