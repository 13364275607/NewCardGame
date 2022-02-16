using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
    public GameObject LoginUI;
    public GameObject NameUI;
    public InputField roomName;
    public InputField playerName;
    // Start is called before the first frame update
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        NameUI.SetActive(true);
    }
    public void PlayButton()
    {
        NameUI.SetActive(false);
        PhotonNetwork.NickName = playerName.text;
        LoginUI.SetActive(true);
    }
    public void JoinOrCreateButton()
    {
        if (roomName.text.Length < 2)
        {
            return;
        }
        LoginUI.SetActive(false);
        RoomOptions options = new RoomOptions { MaxPlayers = 4 };
        PhotonNetwork.JoinOrCreateRoom(roomName.text, options, default);
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel(1);
    }
}
